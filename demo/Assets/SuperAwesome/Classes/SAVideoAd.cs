/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * Class that defines a video ad - that will be finally loaded & displayed by iOS / Android
	 */
	public class SAVideoAd : MonoBehaviour, SALoaderInterface, SANativeInterface {

		/** public variables for the script & prefab */
		private SAAd ad = null;
		public int placementId = 0;
		public bool testModeEnabled = false;
		public bool isParentalGateEnabled = true;
		public bool shouldShowCloseButton = false;
		public bool shouldAutomaticallyCloseAtEnd = true;
		public bool shouldAutoStart = false;

		/** delegates */
		public SAAdInterface adDelegate = null;
		public SAParentalGateInterface parentalGateDelegate = null;
		public SAVideoAdInterface videoAdDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAVideoAd(string unityName, int placementId, string adJson, bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldAutomaticallyCloseAtEnd);
#endif

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

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAVideoAd.SuperAwesomeUnitySAVideoAd(this.name, ad.placementId, ad.adJson, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			Debug.Log("Not in Android yet");
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
			loader.loadAd (placementId);
		}

		/** 
		 * <SALoader> Interface implementation
		 */
		public void didLoadAd(SAAd ad) {

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

		public void didFailToLoadAd(int placementId) {
			Debug.Log("Failure: " + placementId.ToString() );
		}

		/** 
		 * Native callback interface implementation
		 */
		public void nativeCallback(string payload) {
			Dictionary<string, object> payloadDict;
			string type = "";

			/** try to get payload and type data */
			try {
				payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
				type = (string) payloadDict ["type"];
			} catch {
				if (adDelegate != null) {
					adDelegate.adFailedToShow(ad.placementId);
				}
				return;
			}

			switch (type) {
			case "callback_adWasShown":{
				if (adDelegate != null) adDelegate.adWasShown(ad.placementId); break;
			}
			case "callback_adFailedToShow":{
				if (adDelegate != null) adDelegate.adFailedToShow(ad.placementId); break;
			}
			case "callback_adWasClosed":{
				if (adDelegate != null) adDelegate.adWasClosed(ad.placementId); break;
			}
			case "callback_adWasClicked":{
				if (adDelegate != null) adDelegate.adWasClicked(ad.placementId); break;
			}
			case "callback_adHasIncorrectPlacement":{
				if (adDelegate != null) adDelegate.adHasIncorrectPlacement(ad.placementId); break;
			}
			case "callback_parentalGateWasCanceled":{
				if (parentalGateDelegate != null) parentalGateDelegate.parentalGateWasCanceled(ad.placementId); break;
			}
			case "callback_parentalGateWasFailed":{
				if (parentalGateDelegate != null) parentalGateDelegate.parentalGateWasFailed(ad.placementId); break;
			}
			case "callback_parentalGateWasSucceded":{
				if (parentalGateDelegate != null) parentalGateDelegate.parentalGateWasSucceded(ad.placementId); break;
			}
			case "callback_adStarted":{
				if (videoAdDelegate != null) videoAdDelegate.adStarted(ad.placementId); break;
			}
			case "callback_videoStarted":{
				if (videoAdDelegate != null) videoAdDelegate.videoStarted(ad.placementId); break;
			}
			case "callback_videoReachedFirstQuartile":{
				if (videoAdDelegate != null) videoAdDelegate.videoReachedFirstQuartile(ad.placementId); break;
			}
			case "callback_videoReachedMidpoint":{
				if (videoAdDelegate != null) videoAdDelegate.videoReachedMidpoint(ad.placementId); break;
			}
			case "callback_videoReachedThirdQuartile":{
				if (videoAdDelegate != null) videoAdDelegate.videoReachedThirdQuartile(ad.placementId); break;
			}
			case "callback_videoEnded":{
				if (videoAdDelegate != null) videoAdDelegate.videoEnded(ad.placementId); break;
			}
			case "callback_adEnded":{
				if (videoAdDelegate != null) videoAdDelegate.adEnded(ad.placementId); break;
			}
			case "callback_allAdsEnded":{
				if (videoAdDelegate != null) videoAdDelegate.allAdsEnded(ad.placementId); break;
			}
			}
		}
	}
}


