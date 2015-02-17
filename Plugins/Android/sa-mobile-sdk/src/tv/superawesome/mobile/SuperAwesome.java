package tv.superawesome.mobile;

import java.util.List;
import java.util.Observable;

import tv.superawesome.mobile.model.Placement;
import tv.superawesome.mobile.model.Preroll;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.util.Log;

public class SuperAwesome extends Observable implements ISettingsResponse{
	
	private static final String TAG = "SuperAwesome SDK";
	private static final String VERSION = "1.2.0";
	private static final int DEFAULT_APP_ID = 14;

	private static SuperAwesome instance = null;
	
	private Context context;
	private int appId = 0;
	private List<Placement> placements;
	private List<Preroll> prerolls;
	private boolean isLoadingConfiguration = true;
	private boolean useParentalGate = false;
	
	public static String getVersion(){
		return VERSION;
	}
	
	public static SuperAwesome getInstance() {
      if(instance == null) {
         instance = new SuperAwesome();
      }
      return instance;
	}
	
	public SuperAwesome(){
		Log.v(TAG, "SuperAwesome SDK version "+VERSION);
	}
	
	public void setContext(Context context){
		if(this.context == null){
			this.context = context;
			this.appId = getAppId();
			getSettings();
		}
		this.context = context;
	}
	
	public boolean getUseParentalGate(){
		return useParentalGate;
	}
	
	public void setUseParentalgate(boolean useParentalGate){
		this.useParentalGate = useParentalGate;
	}
	
	public void setAppId(int appId){
		this.appId = appId;
	}
	
	public int getAppId(){
		Log.d(TAG, "Looking for app ID...");
		if(this.appId != 0) return appId;
		try {
      ApplicationInfo ai = context.getPackageManager().getApplicationInfo(context.getPackageName(), PackageManager.GET_META_DATA);
      Bundle bundle = ai.metaData;
      Integer appId = bundle.getInt("tv.superawesome.sdk.ApplicationId");
      Log.d(TAG, "appID=" + appId);
      return appId;
    } catch (NameNotFoundException e) {
    	Log.e(TAG, "tv.superawesome.sdk.ApplicationId is not set");
    } catch (NullPointerException e) {
    	Log.e(TAG, "tv.superawesome.sdk.ApplicationId is null");
    }
    	Log.d(TAG, "Using default app ID (" + DEFAULT_APP_ID + ")");
		return DEFAULT_APP_ID;
	}
	
	public void getSettings(){
		SettingsAsyncTask task = new SettingsAsyncTask();
		task.delegate = this;
		task.execute(""+appId);
	}
	
	public boolean getIsLoadingConfiguration(){
		return isLoadingConfiguration;
	}
	
	public Placement getPlacement(String placementID){
		if(placements == null) return null;
		for(Placement p : placements){
			if(p.id.equals(placementID)){
				return p;
			}
		}
		return null;
	}
	
	public Preroll getPreroll(){
		if(prerolls == null) return null;
		if(prerolls.size() == 0) return null;
		return prerolls.get(0);
	}

	@Override
	public void receivedConfiguration(List<Placement> placements, List<Preroll> prerolls) {
		Log.v(TAG, "receivedPlacements");
		this.placements = placements;
		this.prerolls = prerolls;
		this.isLoadingConfiguration = false;
		this.setChanged();
		this.notifyObservers();
	}
}
