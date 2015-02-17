package tv.superawesome.mobile.view;

import java.util.Observable;
import java.util.Observer;

import tv.superawesome.mobile.ParentalGate;
import tv.superawesome.mobile.ParentalGate.ParentalGateViewCallback;
import tv.superawesome.mobile.SuperAwesome;

import android.content.Context;
import android.content.Intent;
import android.content.res.TypedArray;
import android.net.Uri;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.FrameLayout;

import com.adtech.mobilesdk.publisher.configuration.AdtechAdConfiguration;
import com.adtech.mobilesdk.publisher.view.AdtechBannerView;
import com.adtech.mobilesdk.publisher.view.AdtechBannerViewCallback;


public class BannerView extends DisplayAdView implements Observer {
	
	private static final String TAG = "SuperAwesome SDK - Banner";
	private BannerViewListener listener;
	public AdtechBannerView adtechView;
	
	public BannerView(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		SuperAwesome.getInstance().setContext(context);
		
		//Add adtech view
		adtechView = new AdtechBannerView(context);
		LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT);
		adtechView.setLayoutParams(params);
		addView(adtechView);
	}
	
	public BannerViewListener getListener() {
		return listener;
	}

	public void setListener(BannerViewListener listener) {
		this.listener = listener;
	}
	
	private AdtechBannerViewCallback adTechViewCallback = new AdtechBannerViewCallback() {
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
		};
	};
	
	public void setPlacementID(String placementID){
		super.setPlacementID(placementID);
		if(!SuperAwesome.getInstance().getIsLoadingConfiguration()){
			load();
		}else{
			SuperAwesome.getInstance().addObserver(this);
		}
	}

	public void load(){
		AdtechAdConfiguration conf = getConfiguration();
		if(conf != null){
			adtechView.setAdConfiguration(conf);
			adtechView.setViewCallback(adTechViewCallback);
			adtechView.load();
		}
	}
	
	public void stop(){
		adtechView.stop();
	}

	@Override
	public void update(Observable arg0, Object arg1) {
		Log.v(TAG, "Config loaded notification received");
		load();
	}
}