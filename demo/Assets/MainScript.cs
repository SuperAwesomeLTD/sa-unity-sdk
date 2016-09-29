using UnityEngine;
using System.Collections;
using SuperAwesome;

public class MainScript : MonoBehaviour {

	private SABannerAd banner = null;

	// Use this for initialization
	void Start () {
		SuperAwesome.SuperAwesome.instance.handleCPI ();
	}
	
	// Update is called once per frame
	void Update () {
		// do nothing
	}
	
	// button actions
	public void loadAds () {
		banner = SABannerAd.createInstance ();
		banner.setConfigurationStaging ();
		banner.disableParentalGate ();

		banner.load (414);

		SAInterstitialAd.setConfigurationStaging ();
		SAInterstitialAd.setOrientationPortrait ();
		SAInterstitialAd.enableParentalGate ();
		SAInterstitialAd.load (415);
		SAInterstitialAd.load (418);

		SAVideoAd.setConfigurationStaging ();
		SAVideoAd.disableCloseButton ();
		SAVideoAd.enableSmallClickButton ();
		SAVideoAd.setOrientationLandscape ();
		SAVideoAd.load (416);
		SAVideoAd.load (417);
		SAVideoAd.load (28000);
		SAVideoAd.load (2782);

		SAVideoAd.setCallback ((placementId, evt) => {
			switch (evt) {
			case SAEvent.adLoaded:{
				Debug.Log ("Ad loaded for " + placementId);
				break;
			}
			case SAEvent.adFailedToLoad:{ 
				Debug.Log ("Ad failed to load for " + placementId);
				break;
			}
			case SAEvent.adShown:break;
			case SAEvent.adFailedToShow:break;
			case SAEvent.adClicked:break;
			case SAEvent.adClosed:break;
			}
		});

		SAGameWall.setConfigurationStaging ();
		SAGameWall.load (470);
		SAGameWall.load (437);
		SAGameWall.setCallback ((placementId, evt) => {
			Debug.Log ("Event for " + placementId + " ==> " + evt);
		});
	}
	
	public void playBanner () {
		if (banner.hasAdAvailable ()) {
			banner.play ();
		}
	}
	
	public void playInterstitial1 () {
		if (SAInterstitialAd.hasAdAvailable (415)) {
			SAInterstitialAd.play (415);
		}
	}
	
	public void playInterstitial2 () {
		if (SAInterstitialAd.hasAdAvailable (418)) {
			SAInterstitialAd.play (418);
		}
	}

	public void playVideo1 () {
		if (SAVideoAd.hasAdAvailable (416)) {
			SAVideoAd.play (416);
		}
	}

	public void playVideo2 () {
//		if (SAVideoAd.hasAdAvailable (417)) {
//			SAVideoAd.play (417);
//		}
		if (SAGameWall.hasAdAvailable (470)) {
			SAGameWall.play (470);
		} else if (SAGameWall.hasAdAvailable (437)) {
			SAGameWall.play (437);
		}
	}
}
