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
	 * Class that defines an interstitial ad - that will be finally loaded & displayed by iOS / Android
	 */
	public class SAInterstitialAd : MonoBehaviour, SALoaderInterface, SANativeInterface {

		/** public variables for the script & prefab */
		private SAAd ad = null;
		public int placementId = 0;
		public bool testModeEnabled = false;
		public bool isParentalGateEnabled = true;
		public bool shouldAutoStart = false;

		/** delegates */
		public SAAdInterface adDelegate = null;
		public SAParentalGateInterface parentalGateDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAInterstitialAd(string unityName, int placementId, string adJson, bool isParentalGateEnabled);
#endif

		/** static function initialiser */
		public static SAInterstitialAd createInstance() {
			
			/** create a new game object */
			GameObject obj = new GameObject ();
			
			/** add to that new object the video ad */
			SAInterstitialAd adObj = obj.AddComponent<SAInterstitialAd> ();
			adObj.name = "SAInterstitialAd_" + (new System.Random()).Next(100, 1000).ToString();
			
			/** and return the ad Obj instance */
			return adObj;
		}

		/** Use this for initialization */
		void Start (){
			if (shouldAutoStart) {
				showAd(placementId, isParentalGateEnabled);
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
		 * The normal play() function should be used on an instance of the SAInterstitialAd, created with createInstance()
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
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAd(this.name, ad.placementId, ad.adJson, isParentalGateEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			Debug.Log("Not in Android yet");
#else
			Debug.Log ("Open: " + this.name + ", " + ad.placementId);
#endif
		}

		/**
		 * this function <would> be called when starting an interstitial ad from code w/o preloading
		 * or when using the prefab
		 */
		public void showAd(int placementId, bool isParentalGateEnabled) {
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
			 * create another instance of SAInterstitialAd and do all the stuff to play it with
			 * the ad data returned from SALoader
			 */
			SAInterstitialAd iad = SAInterstitialAd.createInstance ();
			iad.setAd(ad);
			iad.isParentalGateEnabled = isParentalGateEnabled;
			iad.play();
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
			}
		}
	}
}


