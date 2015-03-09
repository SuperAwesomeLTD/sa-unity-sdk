package tv.superawesome.mobile.view;

import java.util.HashMap;
import java.util.Map;
import java.util.Observable;
import java.util.Observer;

import tv.superawesome.mobile.SuperAwesome;
import tv.superawesome.mobile.model.Preroll;
import android.content.Context;
import android.util.Log;

import com.google.ads.interactivemedia.v3.api.AdDisplayContainer;
import com.google.ads.interactivemedia.v3.api.AdErrorEvent;
import com.google.ads.interactivemedia.v3.api.AdErrorEvent.AdErrorListener;
import com.google.ads.interactivemedia.v3.api.AdEvent;
import com.google.ads.interactivemedia.v3.api.AdEvent.AdEventListener;
import com.google.ads.interactivemedia.v3.api.AdsLoader;
import com.google.ads.interactivemedia.v3.api.AdsLoader.AdsLoadedListener;
import com.google.ads.interactivemedia.v3.api.AdsManager;
import com.google.ads.interactivemedia.v3.api.AdsManagerLoadedEvent;
import com.google.ads.interactivemedia.v3.api.AdsRequest;
import com.google.ads.interactivemedia.v3.api.ImaSdkFactory;
import com.google.ads.interactivemedia.v3.api.ImaSdkSettings;
import com.google.ads.interactivemedia.v3.samples.demoapp.player.DefaultVideoAdPlayer;

public class VideoViewLoader  implements AdErrorListener, AdsLoadedListener, Observer, AdEventListener{

	private static final String TAG = "SuperAwesome SDK - Video Loader";
	private static int INSTANCE_COUNTER = 0;
	private static Map<Integer,VideoViewLoader> savedInstances = new HashMap<Integer,VideoViewLoader>();
	
	private VideoViewLoaderListener listener = null;
	private boolean isLoaded = false;
	private int instanceId;
	
	protected ImaSdkFactory sdkFactory;
	protected AdsLoader adsLoader;
	protected ImaSdkSettings sdkSettings;
	protected DefaultVideoAdPlayer videoPlayer;
	protected AdsManager adsManager;
	

	public static VideoViewLoader popInstance(int instanceId){
		VideoViewLoader instance = savedInstances.get(instanceId);
		savedInstances.remove(instanceId);
		return instance; 
	}
	
	public VideoViewLoader(Context context){
		this(context, true);
	}
	
	public VideoViewLoader(Context context, boolean saveInstance){
		Log.v(TAG, "VideoViewLoader created");
		
		if(saveInstance){
			instanceId = INSTANCE_COUNTER;
			INSTANCE_COUNTER++;
			savedInstances.put(instanceId, this);
		}
		
		sdkFactory = ImaSdkFactory.getInstance();
		adsLoader = sdkFactory.createAdsLoader(context, getImaSdkSettings());
		adsLoader.addAdErrorListener(this);
		adsLoader.addAdsLoadedListener(this);
		
		videoPlayer = new DefaultVideoAdPlayer(context);
		
		SuperAwesome.getInstance().addObserver(this);
		SuperAwesome.getInstance().setContext(context);
		
		if(!SuperAwesome.getInstance().getIsLoadingConfiguration()){
			requestAd();
		}
	}
	
	public int getInstanceId(){
		return instanceId;
	}
	
	public VideoViewLoaderListener getListener() {
		return listener;
	}

	public void setListener(VideoViewLoaderListener listener) {
		this.listener = listener;
	}
	
	public DefaultVideoAdPlayer getVideoPlayer(){
		return videoPlayer;
	}

	public AdsManager getAdsManager() {
		return adsManager;
	}
	
	public boolean isLoaded(){
		return isLoaded;
	}

	protected ImaSdkSettings getImaSdkSettings() {
    if (sdkSettings == null) {
      sdkSettings = sdkFactory.createImaSdkSettings();
    }
    return sdkSettings;
	}
	
	protected void requestAd() {
		AdDisplayContainer container = sdkFactory.createAdDisplayContainer();
		container.setPlayer(videoPlayer);
	  container.setAdContainer(videoPlayer.getUiContainer());
		
		Log.v(TAG, "Requesting video ad");
	  AdsRequest request = sdkFactory.createAdsRequest();
	  
	  Preroll preroll = SuperAwesome.getInstance().getPreroll();
	  if(preroll == null){
	  	Log.v(TAG, "Using default VAST tag");
		  request.setAdTagUrl("http://u-ads.adap.tv/a/h/0A9CJKYei5h0XHk2xFl36YvivtZ9NIb4jSZkKFRs7AQ=?cb=%7Bcachebreaker%7D&pageUrl=http%3A%2F%2Fadap.tv&eov=eov");
	  }else{
		  request.setAdTagUrl(preroll.vast);
	  }
	  request.setAdDisplayContainer(container);
	  adsLoader.requestAds(request);
	  Log.v(TAG, "Ad requested");
	}
	
	@Override
	public void onAdsManagerLoaded(AdsManagerLoadedEvent event) {
		Log.v(TAG, "Ads manager loaded "+event.toString());
		adsManager = event.getAdsManager();
	  adsManager.addAdErrorListener(this);
	  adsManager.addAdEventListener(this);
	  adsManager.init();
	}

	@Override
	public void onAdError(AdErrorEvent event) {
		Log.v(TAG, "Video Ad Error "+event.getError().getMessage());
		if(listener != null){
			listener.onAdError();
		}
	}

	@Override
	public void update(Observable arg0, Object arg1) {
		requestAd();
	}

	@Override
	public void onAdEvent(AdEvent event) {
		switch (event.getType()) {
    	case LOADED:
    		Log.v(TAG, "Received LOADED event");
    		isLoaded = true;
    		if(listener != null){
    			listener.onLoaded();
    		}
    		break;
    	default:
    		break;
		}
	}
	
}
