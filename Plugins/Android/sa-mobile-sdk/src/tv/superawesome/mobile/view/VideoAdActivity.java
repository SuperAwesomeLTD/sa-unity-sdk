package tv.superawesome.mobile.view;

import android.R;
import android.app.Activity;
import android.app.ProgressDialog;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;
import android.content.Intent;
import android.view.WindowManager;

public class VideoAdActivity extends Activity{
	
	protected static final String TAG = "SuperAwesome SDK - VideoViewActivity";
	private VideoView videoView;
	private ProgressDialog progressDialog;
	
	private VideoViewListener listener = new VideoViewListener() {
	   @Override
	   public void onPlaybackCompleted() {
	  	 Log.v(TAG, "Video ad playback has completed");
	  	 if(progressDialog != null){
	  		 progressDialog.dismiss();
	  	 }
	  	 finish();
	   }
	
	   @Override
	   public void onLoaded() {
	  	 Log.v(TAG, "Video ad has loaded");
	  	 if(progressDialog != null){
	  		 progressDialog.dismiss();
	  	 }
	     videoView.play();
	   }
	   
	   @Override
	   public void onAdError(){
	  	 Log.v(TAG, "Video ad error");
	  	 if(progressDialog != null){
	  		 progressDialog.dismiss();
	  	 }
	  	 finish();
	   }
	 };
	
	@Override
  public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
      // remove title
      requestWindowFeature(Window.FEATURE_NO_TITLE);
      getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
      
      boolean disableLoadingDialog = false;
      if(getIntent().getExtras() != null){
      	disableLoadingDialog = getIntent().getExtras().getBoolean("disable_loading_dialog");
			}
      if(!disableLoadingDialog){
	      progressDialog = new ProgressDialog(this);
	      progressDialog.setMessage("Loading...");
	      progressDialog.setCancelable(false);
	      progressDialog.show();
      }
      
      addVideoView();
 	}
	
	private void addVideoView(){
		videoView = new VideoView(this, null);
    videoView.setBackgroundColor(Color.BLACK);
		videoView.setListener(listener);
		setContentView(videoView);
	}

	public static void openInActivity(Activity activity){
		Intent intent = new Intent(activity, VideoAdActivity.class);
		activity.startActivity(intent);
	}
	
	@Override
	public void onBackPressed() {
		
	}
}
