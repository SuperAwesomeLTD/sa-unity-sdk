using UnityEngine;
using System.Collections;
using SuperAwesome;

public class MainScript : MonoBehaviour {

//	private SABannerAd banner = null;

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
//		banner = SABannerAd.createInstance ();
//		banner.setConfigurationStaging ();
//		banner.disableParentalGate ();
//
//		banner.load (414);

//		SAInterstitialAd.setConfigurationStaging ();
//		SAInterstitialAd.setOrientationPortrait ();
//		SAInterstitialAd.enableParentalGate ();
//		SAInterstitialAd.enableBackButton ();
//		SAInterstitialAd.load (415);
//		SAInterstitialAd.load (418);

		SAVideoAd.setConfigurationProduction ();
		SAVideoAd.enableTestMode ();
		SAVideoAd.enableCloseButton ();
		SAVideoAd.enableSmallClickButton ();
		SAVideoAd.disableBackButton ();
		SAVideoAd.setOrientationLandscape ();
		SAVideoAd.load (32848);

		SAVideoAd.setCallback ((placementId, evt) => {
			switch (evt) {
			case SAEvent.adLoaded:{
				Debug.Log ("Ad loaded for " + placementId);
				break;
			}
			case SAEvent.adFailedToLoad:{ 
				Debug.Log ("adFailedToLoad for " + placementId);
				SAVideoAd.load (32848);
				break;
			}
			case SAEvent.adShown: {
				Debug.Log ("adShown for " + placementId);
				SAVideoAd.load (32848);
				break;
			}
			case SAEvent.adFailedToShow: {
				Debug.Log ("adFailedToShow for " + placementId);
				SAVideoAd.load (32848);
				break;
			}
			case SAEvent.adClicked:break;
			case SAEvent.adClosed:{
				Debug.Log ("adClicked for " + placementId);
				SAVideoAd.load (32848);
				break;
			}
			}
		});

//		SAGameWall.setConfigurationStaging ();
//		SAGameWall.enableBackButton ();
//		SAGameWall.load (470);
//		SAGameWall.load (437);
//		SAGameWall.setCallback ((placementId, evt) => {
//			Debug.Log ("Event for " + placementId + " ==> " + evt);
//		});
	}
	
	public void playBanner () {
//		if (banner.hasAdAvailable ()) {
//			banner.play ();
//		}
	}
	
	public void playInterstitial1 () {
//		if (SAInterstitialAd.hasAdAvailable (415)) {
//			SAInterstitialAd.play (415);
//		}
	}
	
	public void playInterstitial2 () {
//		if (SAInterstitialAd.hasAdAvailable (418)) {
//			SAInterstitialAd.play (418);
//		}
	}

	public void playVideo1 () {
		if (SAVideoAd.hasAdAvailable (32848)) {
			SAVideoAd.play (32848);
		}
	}

	public void playVideo2 () {
//		if (SAVideoAd.hasAdAvailable (417)) {
//			SAVideoAd.play (417);
//		}
//		if (SAGameWall.hasAdAvailable (470)) {
//			SAGameWall.play (470);
//		} else if (SAGameWall.hasAdAvailable (437)) {
//			SAGameWall.play (437);
//		}
	}
}
