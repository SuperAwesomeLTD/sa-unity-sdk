using UnityEngine;
using System.Collections;

using tv.superawesome.sdk.publisher;

public class MainScript : MonoBehaviour {

	private SABannerAd banner = null;

	// Use this for initialization
	void Start () {
		// do nothing
	}
	
	// Update is called once per frame
	void Update () {
		// do nothing
	}
	
	// button actions
	public void loadAds () {


//		SAInterstitialAd.setConfigurationStaging ();
//		SAInterstitialAd.setOrientationPortrait ();
//		SAInterstitialAd.enableParentalGate ();
//		SAInterstitialAd.enableBackButton ();
//		SAInterstitialAd.load (415);
//		SAInterstitialAd.load (418);

//		SAVideoAd.setConfigurationProduction ();
//		SAVideoAd.enableTestMode ();
//		SAVideoAd.enableCloseButton ();
//		SAVideoAd.enableSmallClickButton ();
//		SAVideoAd.disableBackButton ();
//		SAVideoAd.setOrientationLandscape ();
//		SAVideoAd.load (32848);
//
//		SAVideoAd.setCallback ((placementId, evt) => {
//			switch (evt) {
//			case SAEvent.adLoaded:{
//				Debug.Log ("Ad loaded for " + placementId);
//				break;
//			}
//			case SAEvent.adFailedToLoad:{ 
//				Debug.Log ("adFailedToLoad for " + placementId);
//				SAVideoAd.load (32848);
//				break;
//			}
//			case SAEvent.adShown: {
//				Debug.Log ("adShown for " + placementId);
//				SAVideoAd.load (32848);
//				break;
//			}
//			case SAEvent.adFailedToShow: {
//				Debug.Log ("adFailedToShow for " + placementId);
//				SAVideoAd.load (32848);
//				break;
//			}
//			case SAEvent.adClicked:break;
//			case SAEvent.adClosed:{
//				Debug.Log ("adClicked for " + placementId);
//				SAVideoAd.load (32848);
//				break;
//			}
//			}
//		});
//
//		SAAppWall.setConfigurationStaging ();
//		SAAppWall.enableBackButton ();
//		SAAppWall.load (470);
//		SAAppWall.load (437);
//		SAAppWall.setCallback ((placementId, evt) => {
//			Debug.Log ("Event for " + placementId + " ==> " + evt);
//		});
	}
	
	public void playBanner () {
		banner = SABannerAd.createInstance ();
		banner.setConfigurationProduction ();
		banner.enableTestMode ();
		banner.disableParentalGate ();
		banner.load (30989);
	}
	
	public void playInterstitial1 () {
		if (banner.hasAdAvailable()) {
			banner.play();
		}
	}
	
	public void playInterstitial2 () {
		banner.close ();
	}

	public void playVideo1 () {
//		if (SAVideoAd.hasAdAvailable (32848)) {
//			SAVideoAd.play (32848);
//		}
	}

	public void playVideo2 () {
//		if (SAVideoAd.hasAdAvailable (417)) {
//			SAVideoAd.play (417);
//		}
//		if (SAAppWall.hasAdAvailable (470)) {
//			SAAppWall.play (470);
//		} else if (SAAppWall.hasAdAvailable (437)) {
//			SAAppWall.play (437);
//		}
	}
}
