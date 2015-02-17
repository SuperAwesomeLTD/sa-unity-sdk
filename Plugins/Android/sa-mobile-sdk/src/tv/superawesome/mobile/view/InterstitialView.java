package tv.superawesome.mobile.view;

import java.util.Observable;
import java.util.Observer;

import tv.superawesome.mobile.ParentalGate;
import tv.superawesome.mobile.SuperAwesome;
import tv.superawesome.mobile.ParentalGate.ParentalGateViewCallback;

import com.adtech.mobilesdk.publisher.configuration.AdtechAdConfiguration;
import com.adtech.mobilesdk.publisher.view.AdtechInterstitialView;
import com.adtech.mobilesdk.publisher.view.AdtechInterstitialViewCallback;

import android.content.Context;
import android.content.Intent;
import android.content.res.TypedArray;
import android.net.Uri;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.FrameLayout;


public class InterstitialView extends DisplayAdView implements Observer{
	
	private static final String TAG = "SuperAwesome SDK - Interstitial";
	private InterstitialViewListener listener;
	private boolean shouldPresentOnLoad = false;
	public AdtechInterstitialView adtechView;
	
	public InterstitialView(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		adtechView = new AdtechInterstitialView(context);
		LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT);
		adtechView.setLayoutParams(params);
		addView(adtechView);
		
		//Set observer
		SuperAwesome.getInstance().addObserver(this);
		SuperAwesome.getInstance().setContext(context);
	}
	
	public InterstitialViewListener getListener() {
		return listener;
	}

	public void setListener(InterstitialViewListener listener) {
		this.listener = listener;
	}

	private AdtechInterstitialViewCallback atcb = new AdtechInterstitialViewCallback(){
    	@Override
    	public void onAdSuccess() {
    		if(getListener() != null){
    			getListener().onLoaded();
    		}
    	}
    	
    	@Override
    	public void onAdDismiss(){
    		if(getListener() != null){
    			getListener().onAdDismiss();
    		}
    	}
    	
    	@Override
    	public void onAdLeave() {
    		if(getListener() != null){
    			getListener().onAdLeave();
    		}
    	}
    	
    	@Override
    	public void onAdFailure() {
    		if(getListener() != null){
    			getListener().onAdError();
    		}
    	}
    	
    	@Override
    	public boolean shouldInterceptLandingPageOpening(final String url, NonModalLandingPageHandlerCallback callback) {
    		if(!SuperAwesome.getInstance().getUseParentalGate()) return false;
			
				ParentalGate gate = new ParentalGate(getContext());
				ParentalGateViewCallback cb = new ParentalGateViewCallback(){
					public void onCorrectAnswer(){
						Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(url));
						getContext().startActivity(browserIntent);
					}
				};
				gate.setViewCallback(cb);
				return true;
    	}
	};
	
	public void present(){
		if(SuperAwesome.getInstance().getIsLoadingConfiguration()){
			shouldPresentOnLoad = true;
			return;
		}
		AdtechAdConfiguration conf = getConfiguration();
		if(conf != null){
			adtechView.setAdConfiguration(conf);
			adtechView.setViewCallback(atcb);
    	adtechView.load();
		}
	}

	@Override
	public void update(Observable arg0, Object arg1) {
		Log.v(TAG, "Config loaded notification received");
		if(shouldPresentOnLoad){
			Log.v(TAG, "Presenting now");
			present();
			shouldPresentOnLoad = false;
		}
	}

}
