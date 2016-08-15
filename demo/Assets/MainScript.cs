using UnityEngine;
using System.Collections;
using SuperAwesome;

public class MainScript : MonoBehaviour, SALoaderInterface, SAAdInterface, SAParentalGateInterface, SAVideoAdInterface  {

	private SALoader loader = null, loader1 = null;
	private SAAd adBanner = null;
	private SAAd adInterstitial = null;
	private SAAd adVideo = null;
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
		
		SuperAwesome.SuperAwesome.instance.setConfigurationStaging ();
		SuperAwesome.SuperAwesome.instance.disableTestMode ();

		loader = SALoader.createInstance ();
		loader.loaderDelegate = this;
		loader.loadAd (113);	// banner
		SuperAwesome.SuperAwesome.instance.enableTestMode ();
		loader.loadAd (117); 	// interstitial

		SuperAwesome.SuperAwesome.instance.setConfigurationProduction ();
		SuperAwesome.SuperAwesome.instance.enableTestMode ();

		loader1 = SALoader.createInstance ();
		loader1.loaderDelegate = this;
		loader1.loadAd (28000);  // video
		
	}
	
	public void playBanner () {
		vad = SAVideoAd.createInstance ();
		vad.setAd (adVideo);
		vad.adDelegate = this;
		vad.videoAdDelegate = this;
		vad.shouldShowSmallClickButton = true;
		vad.shouldLockOrientation = true;
		vad.shouldShowCloseButton = false;
		vad.shouldAutomaticallyCloseAtEnd = true;
		vad.lockOrientation = SALockOrientation.PORTRAIT;
		vad.play ();
	}
	
	public void deleteBanner () {
		if (bad != null) {
			bad.close();
		}
	}
	
	public void playInterstitial () {
		iad = SAInterstitialAd.createInstance ();
		iad.setAd (adInterstitial);
		iad.isParentalGateEnabled = true;
		iad.adDelegate = this;
		iad.parentalGateDelegate = this;
		iad.shouldLockOrientation = true;
		iad.lockOrientation = SALockOrientation.PORTRAIT;
		iad.play ();
	}
	
	/** <SALoaderInterface> */
	public void didLoadAd(SAAd ad) {
		if (ad.placementId == 113) {
			bad = SABannerAd.createInstance ();
			bad.setAd (ad);
			bad.position = SABannerAd.BannerPosition.TOP;
			bad.size = SABannerAd.BannerSize.BANNER_728_90;
			bad.color = SABannerAd.BannerColor.BANNER_GRAY;
			bad.isParentalGateEnabled = false;
			bad.adDelegate = this;
			bad.parentalGateDelegate = this;
			bad.play ();
		} 
		else 
		if (ad.placementId == 117) {
			adInterstitial = ad;
		} 
		else 
		if (ad.placementId == 28000) {
			adVideo = ad;
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

	public void adStarted(int placementId) {
		Debug.Log ("[Unity] - adStarted " + placementId);
	}
	
	public void videoStarted(int placementId) {
		Debug.Log ("[Unity] - videoStarted " + placementId);
	}
	
	public void videoReachedFirstQuartile(int placementId) {
		Debug.Log ("[Unity] - videoReachedFirstQuartile " + placementId);
	}
	
	public void videoReachedMidpoint(int placementId) {
		Debug.Log ("[Unity] - videoReachedMidpoint " + placementId);
	}
	
	public void videoReachedThirdQuartile(int placementId) {
		Debug.Log ("[Unity] - videoReachedThirdQuartile " + placementId);
	}
	
	public void videoEnded(int placementId) {
		Debug.Log ("[Unity] - videoEnded " + placementId);
	}
	
	public void adEnded(int placementId) {
		Debug.Log ("[Unity] - adEnded " + placementId);
	}
	
	public void allAdsEnded(int placementId) {
		Debug.Log ("[Unity] - parentalGateWasSucceded " + placementId);
	}
}
