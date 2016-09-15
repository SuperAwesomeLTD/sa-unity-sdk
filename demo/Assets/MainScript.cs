using UnityEngine;
using System.Collections;
using SuperAwesome;

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
		if (SAVideoAd.hasAdAvailable (417)) {
			SAVideoAd.play (417);
		}
	}
}
