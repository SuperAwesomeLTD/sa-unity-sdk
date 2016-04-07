using UnityEngine;
using System;
using System.Collections;

namespace SuperAwesome {

	public class onBtnClick : MonoBehaviour, SALoaderInterface, SAAdInterface, SAParentalGateInterface {

		private SALoader loader1 = null, loader2 = null;
		private SAAd adBanner = null;
		private SAAd adInterstitial = null;
		private SABannerAd bad = null;
		private SAInterstitialAd iad = null;

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

			SuperAwesome.instance.setConfigurationProduction ();
			SuperAwesome.instance.enableTestMode ();

			loader1 = SALoader.createInstance ();
			loader1.loaderDelegate = this;
			loader1.loadAd (31107);	// 728x90

			loader2 = SALoader.createInstance ();
			loader2.loaderDelegate = this;
			loader2.loadAd (31108);	// interstitial
		}

		public void playBanner () {
			if (adBanner != null) {
				bad = SABannerAd.createInstance();
				bad.setAd(adBanner);
				bad.position = SABannerAd.BannerPosition.BOTTOM;
				bad.size = SABannerAd.BannerSize.BANNER_728_90;
				bad.color = SABannerAd.BannerColor.BANNER_GRAY;
				bad.isParentalGateEnabled = true;
				bad.adDelegate = this;
				bad.parentalGateDelegate = this;
				bad.play();
			}
		}

		public void deleteBanner () {
			if (bad != null) {
				bad.close ();
			}
		}

		public void playInterstitial () {
			if (adInterstitial != null) {
				iad = SAInterstitialAd.createInstance();
				iad.setAd(adInterstitial);
				iad.isParentalGateEnabled = true;
				iad.adDelegate = this;
				iad.parentalGateDelegate = this;
				iad.play();
			}
		}

		/** <SALoaderInterface> */
		public void didLoadAd(SAAd ad) {
			if (ad.placementId == 31107) {
				adBanner = ad;
			} else if (ad.placementId == 31108) {
				adInterstitial = ad;
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
}

