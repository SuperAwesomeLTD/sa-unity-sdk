package tv.superawesome.mobile.view;

import tv.superawesome.mobile.R;
import tv.superawesome.mobile.SuperAwesome;
import tv.superawesome.mobile.R.styleable;
import tv.superawesome.mobile.model.Placement;

import com.adtech.mobilesdk.publisher.configuration.AdtechAdConfiguration;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.util.Log;
import android.widget.FrameLayout;

public class DisplayAdView extends FrameLayout{
	
	private static final String TAG = "SuperAwesome SDK - DisplayAd";
	private String placementID;

	public DisplayAdView(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		//Get attributes from xml
		TypedArray arr = context.obtainStyledAttributes(attrs, R.styleable.DisplayAdView);
		CharSequence attr = arr.getString(R.styleable.DisplayAdView_placementID);
		if (attr != null) {
			setPlacementID(attr.toString());
		}
		arr.recycle();
	}
	
	public String getPlacementID() {
		return placementID;
	}

	public void setPlacementID(String placementID) {
		this.placementID = placementID;
	}
	
	protected AdtechAdConfiguration getConfiguration(){
		if(getPlacementID() == null){
			Log.e(TAG, "Placement ID is not set, could not load ad");
			return null;
		}
		Placement placement = SuperAwesome.getInstance().getPlacement(getPlacementID());
		if(placement == null){
			Log.e(TAG, "Could not find configuration for placement ID");
			return null;
		}
		AdtechAdConfiguration config = new AdtechAdConfiguration("MyApp");
		config.setAlias(placement.alias);
		config.setDomain("a.adtech.de");
		config.setNetworkId(placement.networkId);
		config.setSubnetworkId(placement.subNetworkId);
		return config;
	}

}
