package tv.superawesome.mobile;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import tv.superawesome.mobile.model.Placement;
import tv.superawesome.mobile.model.Preroll;

import android.os.AsyncTask;
import android.util.Log;


public class SettingsAsyncTask extends AsyncTask<String, String, List<Placement>> {
	
	private static final String TAG = "SuperAwesome SDK";
	public ISettingsResponse delegate=null;
	private String response;
	private List<Placement> placements;
	private List<Preroll> prerolls;
	
	 private String readStream(InputStream is) {
	    try {
	      ByteArrayOutputStream bo = new ByteArrayOutputStream();
	      int i = is.read();
	      while(i != -1) {
	        bo.write(i);
	        i = is.read();
	      }
	      return bo.toString();
	    } catch (IOException e) {
	      return "";
	    }
	}
	 
	 private void writeStream(OutputStream outputstream, String s) throws IOException
    {
        outputstream.write(s.getBytes());
        outputstream.close();
    }
	 
	private boolean redirected(HttpURLConnection conn) throws IOException{
		int status = conn.getResponseCode();
    	if (status != HttpURLConnection.HTTP_OK) {
    		if (status == HttpURLConnection.HTTP_MOVED_TEMP || status == HttpURLConnection.HTTP_MOVED_PERM || status == 307)
    		return true;
    	}
    	return false;
	}
	
	private void postData(String appId){
	    // Create a new HttpClient and Post Header
	    HttpClient httpclient = new DefaultHttpClient();
	    HttpPost httppost = new HttpPost("http://dashboard.superawesome.tv/api/sdk/ads");
	    // httppost = new HttpPost("http://staging.dashboard.superawesome.tv/api/sdk/ads");
	    
	    try {
	        // Add your data
	        List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
	        nameValuePairs.add(new BasicNameValuePair("app_id", appId));
	        httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));

	        // Execute HTTP Post Request
	        HttpResponse response = httpclient.execute(httppost);
	        this.response = readStream(response.getEntity().getContent());
	        
	        Log.d("SuperAwesome SDK", "response: "+this.response);
	        
	    } catch (ClientProtocolException e) {
	    	Log.d("SuperAwesome SDK", "Error "+e.getLocalizedMessage());
	    } catch (IOException e) {
	    	Log.d("SuperAwesome SDK", "Error "+e.getLocalizedMessage());
	    }
	}
	
	private void processPlacements() throws JSONException{
		JSONObject json = new JSONObject(this.response);
        boolean success = (Boolean) json.get("success");
        JSONArray ads = json.getJSONArray("ads");
        placements = new ArrayList<Placement>();
        for (int i = 0; i < ads.length(); i++) {
    	  JSONObject ad = ads.getJSONObject(i);
    	  Placement placement = new Placement();
    	  placement.id = ad.getString("id");
    	  placement.name = ad.getString("name");
    	  placement.networkId = ad.getInt("networkId");
    	  placement.subNetworkId = ad.getInt("subNetworkId");
    	  placement.alias = ad.getString("alias");
    	  placement.width = ad.getInt("width");
    	  placement.height = ad.getInt("height");
    	  placements.add(placement);
    	  Log.d(TAG, "ad: "+placement.id+ " w:"+placement.width+ " h:"+placement.height);
    	}
	}
	
	private void processPrerolls() throws JSONException{
		JSONObject json = new JSONObject(this.response);
		JSONArray prerollsJson = json.getJSONArray("prerolls");
        prerolls = new ArrayList<Preroll>();
        for (int i = 0; i < prerollsJson.length(); i++) {
        	JSONObject prerollJson = prerollsJson.getJSONObject(i);
        	Preroll preroll = new Preroll();
        	preroll.id = prerollJson.getInt("id");
        	preroll.vast = prerollJson.getString("vast");
        	prerolls.add(preroll);
        	Log.d(TAG, "preroll: "+preroll.id+ " vast:"+preroll.vast);
        }
	}
	
	@Override
	protected List<Placement> doInBackground(String... params) {
		try {
			postData(params[0]);
			
			try {
				processPlacements();
			} catch (JSONException e) {
				Log.d(TAG, "Could not process display placements");
			}
			try {
				processPrerolls();
			} catch (JSONException e) {
				Log.d(TAG, "Could not process video placements");
			}
			
		} catch (Exception ex) {
			Log.d(TAG, "Could not post data to server");
		}

		return placements;
	}

	protected void onPostExecute(List<Placement> result) {
		delegate.receivedConfiguration(placements, prerolls);
	}

}
