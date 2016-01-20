using UnityEngine;
using System;
using System.Collections;

namespace SuperAwesome {

	public class onBtnClick : MonoBehaviour, SALoaderInterface, SAAdInterface {

		private SALoader loader = null;
		private SAAd ad = null;
		private bool isAdLoaded = false;

		// Use this for initialization
		void Start () {
			// do nothing
		}
		
		// Update is called once per frame
		void Update () {
			// do nothing
		}
		
		// button actions
		public void loadVideoAction () {

			loader = SALoader.createInstance ();
			loader.loaderDelegate = this;
			loader.loadAd (28000);
		}

		public void playVideoAction () {
			if (ad != null) {
				SAVideoAd vad = SAVideoAd.createInstance ();
				vad.setAd(ad);
				vad.isParentalGateEnabled = true;
				vad.shouldShowCloseButton = false;
				vad.shouldAutomaticallyCloseAtEnd = true;
				vad.adDelegate = this;
				vad.play();
			} else {
				Debug.Log("Ad is still not loaded!");
			}
		}
		
		public void playVideoDirectlyAction () {
			SAVideoAd.createInstance().showAd (28000, false, true, false);
		}

		public void playInterstitial() {
			SAInterstitialAd.createInstance ().showAd (25397, true);
		}

		/** <SALoaderInterface> */
		void SALoaderInterface.didLoadAd(SAAd ad) {
			this.ad = ad;
			this.isAdLoaded = true;
		}

		void SALoaderInterface.didFailToLoadAd(int placementId) {
			Debug.Log ("Could not load " + placementId);
		}

		/** <SAAdInterface> */
		void SAAdInterface.adWasShown(int placementId) {
			Debug.Log ("[Unity] - adWasShown");
		}

		void SAAdInterface.adFailedToShow(int placementId) {
			Debug.Log ("[Unity] - adFailedToShow");
		}

		void SAAdInterface.adWasClosed(int placementId) {
			Debug.Log ("[Unity] - adWasClosed");
		}

		void SAAdInterface.adWasClicked(int placementId) {
			Debug.Log ("[Unity] - adWasClicked");
		}

		void SAAdInterface.adHasIncorrectPlacement(int placementId) {
			Debug.Log ("[Unity] - adHasIncorrectPlacement");
		}
	}
}

