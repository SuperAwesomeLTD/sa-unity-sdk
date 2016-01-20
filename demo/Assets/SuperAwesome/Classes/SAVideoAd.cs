using UnityEngine;
using System.Collections;

namespace SuperAwesome {
	public class SAVideoAd : MonoBehaviour, SALoaderInterface {

		/** public variables for the script & prefab */
		private SAAd ad = null;
		public int placementId = 0;
		public bool testModeEnabled = false;
		public bool isParentalGateEnabled = true;
		public bool shouldShowCloseButton = false;
		public bool shouldAutomaticallyCloseAtEnd = true;
		public bool shouldAutoStart = false;

		/** static function initialiser */
		public static SAVideoAd createInstance() {
			/** create a new game object */
			GameObject obj = new GameObject ();

			/** add to that new object the video ad */
			SAVideoAd adObj = obj.AddComponent<SAVideoAd> ();
			adObj.name = "SAVideoAd_" + (new System.Random()).Next(100, 1000).ToString();

			/** and return the ad Obj instance */
			return adObj;
		}

		/** Use this for initialization */
		void Start () {
			if (shouldAutoStart) {
				showAd(placementId, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
			}
		}
		
		/** Update is called once per frame */
		void Update () {
			/** do nothing */
		}

		/** setter for the Ad */
		public void setAd(SAAd ad) {
			this.ad = ad;
		}

		/**
		 * The normal play() function should be used on an instance of the SAVideoAd, created with createInstance()
		 * and whose ad data has been pre-loaded using SALoader
		 */
		public void play () {
			if (ad == null) {
				Debug.Log("Tried to play ad without ad data for " + this.name);
				return;
			}

			if (ad.placementId == -1 || ad.placementId == 0 || ad.placementId == null) {
				Debug.Log("Tried to play ad without ad data for " + this.name);
				return;
			}

			#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
			SABridge.openVideoAd(this.name, ad.placementId, ad.adJson, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
			#else
			Debug.Log ("Open: " + this.name + ", " + ad.placementId);
			#endif
		}

		/**
		 * this function <would> be called when starting a video ad from code w/o preloading
		 * or when using the prefab
		 */
		public void showAd(int placementId, bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldAutomaticallyCloseAtEnd) {
			/** create an instance of SALoader */
			SALoader loader = SALoader.createInstance ();
			/** set delegate methods */
			loader.loaderDelegate = this;
			/** load the actual ad */
			loader.loadAd (28000);
		}

		/** <SALoaderInterface> implementation */
		void SALoaderInterface.didLoadAd(SAAd ad) {

			/** 
			 * create another instance of SAVideoAd and do all the stuff to play it with
			 * the ad data returned from SALoader
			 */
			SAVideoAd vad = SAVideoAd.createInstance ();
			vad.setAd(ad);
			vad.isParentalGateEnabled = isParentalGateEnabled;
			vad.shouldShowCloseButton = shouldShowCloseButton;
			vad.shouldAutomaticallyCloseAtEnd = shouldAutomaticallyCloseAtEnd;
			vad.play();
		}

		void SALoaderInterface.didFailToLoadAd(int placementId) {
			Debug.Log("Failure: " + placementId.ToString() );
		}
	}
}



