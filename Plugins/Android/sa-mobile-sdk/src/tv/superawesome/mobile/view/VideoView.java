package tv.superawesome.mobile.view;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.FrameLayout;

import com.google.ads.interactivemedia.v3.samples.demoapp.player.DefaultVideoAdPlayer;
import com.google.ads.interactivemedia.v3.samples.demoapp.player.TrackingVideoView.CompleteCallback;

public class VideoView extends FrameLayout implements VideoViewLoaderListener, CompleteCallback {
	
	private static final String TAG = "SuperAwesome SDK - Video";
	
	protected VideoViewListener listener; 
	
	private VideoViewLoader loader;
	private DefaultVideoAdPlayer player;

	public VideoView(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		Log.v(TAG, "VideoView created");
		
		setLoader(new VideoViewLoader(context, false));
	}
	
	public VideoView(Context context, AttributeSet attrs, VideoViewLoader loader) {
		super(context, attrs);
		
		Log.v(TAG, "VideoView created with loader");
		
		setLoader(loader);
	}
	
	public void setListener(VideoViewListener listener){
		this.listener = listener;
	}
	
	private void setLoader(VideoViewLoader loader){
		this.loader = loader;
		loader.setListener(this);
		
		if(loader.isLoaded()){
			onLoaded();
		}
	}
	
	@Override
	public void onLoaded() {
		player = loader.getVideoPlayer();
		player.setCompletionCallback(this);
		addView(player);
		if(listener != null){
  		listener.onLoaded();
  	}
	}
	
	@Override
	public void onAdError() {
		Log.v(TAG, "Video Ad Error");
	  if(listener != null){
  		listener.onAdError();
  	}
	}
	
	@Override
	public void onComplete() {
		if(listener != null){
			listener.onPlaybackCompleted();
    }
	}
	
	public void play(){
		loader.getAdsManager().start();
	}

}
