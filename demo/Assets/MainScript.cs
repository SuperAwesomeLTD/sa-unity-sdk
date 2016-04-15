using UnityEngine;
using System.Collections;
using SuperAwesome;

public class MainScript : MonoBehaviour, SALoaderInterface, SAAdInterface, SAParentalGateInterface  {

	private SALoader loader = null, loader1 = null;
	private SAAd adBanner = null;
	private SAAd adInterstitial = null;
	private SABannerAd bad = null;
	private SAInterstitialAd iad = null;
	private SAVideoAd vad = null;
	
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
		
		SuperAwesome.SuperAwesome.instance.setConfigurationProduction ();
		SuperAwesome.SuperAwesome.instance.enableTestMode ();
		
		loader = SALoader.createInstance ();
		loader.loaderDelegate = this;
		loader.loadAd (31107);	// 728x90
		loader.loadAd (31108);  // video
		
		loader1 = SALoader.createInstance ();
		loader1.loaderDelegate = this;
		loader1.loadAd (28000);  // interstitial
		
	}
	
	public void playBanner () {
		
	}
	
	public void deleteBanner () {
		
	}
	
	public void playInterstitial () {
		iad = SAInterstitialAd.createInstance ();
		iad.setAd (adInterstitial);
		iad.isParentalGateEnabled = true;
		iad.adDelegate = this;
		iad.parentalGateDelegate = this;
		iad.play ();
	}
	
	/** <SALoaderInterface> */
	public void didLoadAd(SAAd ad) {
		if (ad.placementId == 31107) {
			bad = SABannerAd.createInstance ();
			bad.setAd (ad);
			bad.position = SABannerAd.BannerPosition.BOTTOM;
			bad.size = SABannerAd.BannerSize.BANNER_728_90;
			bad.color = SABannerAd.BannerColor.BANNER_GRAY;
			bad.isParentalGateEnabled = true;
			bad.adDelegate = this;
			bad.parentalGateDelegate = this;
			bad.play ();
		} else if (ad.placementId == 31108) {
			adInterstitial = ad;
		} else if (ad.placementId == 28000) {
			vad = SAVideoAd.createInstance ();
			vad.setAd (ad);
			vad.play ();
		}
	}
	
	public void didFailToLoadAd(int placementId) {
		Debug.Log ("[Unity] - didFailToLoadAd " + placementId);
	}
	
	/** <SAAdInterface> */
	public void adWasShown(int placementId) {
		Debug.Log ("[Unity] - adWasShown " + placementId);
	}
	
	public void adFailedToShow(int placementId) {
		Debug.Log ("[Unity] - adFailedToShow" + placementId);
	}
	
	public void adWasClosed(int placementId) {
		Debug.Log ("[Unity] - adWasClosed " + placementId);
	}
	
	public void adWasClicked(int placementId) {
		Debug.Log ("[Unity] - adWasClicked " + placementId);
	}
	
	public void adHasIncorrectPlacement(int placementId) {
		Debug.Log ("[Unity] - adHasIncorrectPlacement");
	}
	
	public void parentalGateWasCanceled(int placementId) {
		Debug.Log ("[Unity] - parentalGateWasCanceled " + placementId);
	}
	
	public void parentalGateWasFailed(int placementId) {
		Debug.Log ("[Unity] - parentalGateWasFailed " + placementId);
	}
	
	public void parentalGateWasSucceded(int placementId) {
		Debug.Log ("[Unity] - parentalGateWasSucceded " + placementId);
	}
}
