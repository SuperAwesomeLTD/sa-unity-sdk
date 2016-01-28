/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * Class that defines a banner ad - that will be finally loaded & displayed by iOS / Android
	 */
	public class SABannerAd : MonoBehaviour, SALoaderInterface, SANativeInterface {

		/** public variables for the script & prefab */
		private SAAd ad = null;
		public int placementId = 0;
		public bool testModeEnabled = false;
		public bool isParentalGateEnabled = true;
		public bool shouldAutoStart = false;

		/** public enum for banner predefined positions */
		public enum BannerPosition {
			TOP = 0,
			BOTTOM = 1
		}
		public BannerPosition position = BannerPosition.BOTTOM;
		
		/** public enum for the banner predefined sized */
		public enum BannerSize {
			BANNER_320_50 = 0,
			BANNER_300_50 = 1,
			BANNER_728_90 = 2,
			BANNER_300_250 = 3
		}
		public BannerSize size = BannerSize.BANNER_320_50;

		/** delegates */
		public SAAdInterface adDelegate = null;
		public SAParentalGateInterface parentalGateDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySABannerAd(int placementId, string adJson, string unityName, int position, int size, bool isParentalGateEnabled);
#endif

		/** static function initialiser */
		public static SABannerAd createInstance() {
			
			/** create a new game object */
			GameObject obj = new GameObject ();

			/** add to that new object the video ad */
			SABannerAd adObj = obj.AddComponent<SABannerAd> ();
			adObj.name = "SABannerAd_" + (new System.Random()).Next(100, 1000).ToString();
			
			/** and return the ad Obj instance */
			return adObj;
		}
		
		/** Use this for initialization */
		void Start (){

			/** make the color invisible when playing */
			if (this.GetComponent<Image> () != null) {
				Color current = this.GetComponent<Image>().color;
				current.a = 0;
				this.GetComponent<Image>().color = current;
			}

			/** check for autostart and then start */
			if (shouldAutoStart) {
				showAd(placementId, position, size, isParentalGateEnabled);
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
			SABannerAd.SuperAwesomeUnitySABannerAd(ad.placementId, ad.adJson, this.name, (int)position, (int)size, isParentalGateEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var uname = this.name; 
			
			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySABannerAd", context, ad.placementId, ad.adJson, uname, (int)position, (int)size, isParentalGateEnabled);
			}));
#else
			Debug.Log ("Open: " + this.name + ", " + ad.placementId);
#endif
		}
		
		/**
		 * this function <would> be called when starting an interstitial ad from code w/o preloading
		 * or when using the prefab
		 */
		private void showAd(int placementId, BannerPosition position, BannerSize size, bool isParentalGateEnabled) {
			/** assign vars */
			this.placementId = placementId;
			this.position = position;
			this.size = size;
			this.isParentalGateEnabled = isParentalGateEnabled;

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
			this.ad = ad;
			this.play ();
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



