/** 
 * Imports used for this class
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * Class that defines a video ad - that will be finally loaded & displayed by iOS / Android
	 */
	public class SAVideoAd : MonoBehaviour, SALoaderInterface, SAViewInterface, SANativeInterface {

		/** instance index */
		private static uint index = 0;

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
		private static extern void SuperAwesomeUnitySAVideoAd(int placementId, string adJson, string unityName, bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldAutomaticallyCloseAtEnd);
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnityCloseSAFullscreenVideoAd(string unityName);
#endif

		/*********************************************************************************************/
		/** Normal Unity Init methods */
		/*********************************************************************************************/

		/** static function initialiser */
		public static SAVideoAd createInstance() {

			/** create a new game object */
			GameObject obj = new GameObject ();

			/** add to that new object the video ad */
			SAVideoAd adObj = obj.AddComponent<SAVideoAd> ();
			adObj.name = "SAVideoAd_" + (++SAVideoAd.index);

			/** call don't destroy on load ... */
			DontDestroyOnLoad (obj);

			/** and return the ad Obj instance */
			return adObj;
		}

		/** Use this for initialization */
		void Start () {
			/** make the color invisible when playing */
			if (this.GetComponent<Image> () != null) {
				Color current = this.GetComponent<Image>().color;
				current.a = 0;
				this.GetComponent<Image>().color = current;
			}
			
			/** check for autostart and then start */
			if (shouldAutoStart) {
				showAd(placementId, testModeEnabled, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
			}
		}
		
		/** Update is called once per frame */
		void Update () {
			/** do nothing */
		}

		/*********************************************************************************************/
		/** Internal loader methods */
		/*********************************************************************************************/

		/**
		 * this function <would> be called when starting a video ad from code w/o preloading
		 * or when using the prefab
		 */
		private void showAd(int placementId, bool testModeEnabled,  bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldAutomaticallyCloseAtEnd) {
			/** assign vars */
			this.placementId = placementId;
			this.isParentalGateEnabled = isParentalGateEnabled;
			this.shouldShowCloseButton = shouldShowCloseButton;
			this.shouldAutomaticallyCloseAtEnd = shouldAutomaticallyCloseAtEnd;
			this.testModeEnabled = testModeEnabled;

			/** save the current global test mode - and assign the new one */
			bool cTestMode = SuperAwesome.instance.isTestingEnabled ();
			SuperAwesome.instance.setTestMode (this.testModeEnabled);

			/** create an instance of SALoader */
			SALoader loader = SALoader.createInstance ();

			/** set delegate methods */
			loader.loaderDelegate = this;

			/** load the actual ad */
			loader.loadAd (placementId);

			/** revert to current global test mode */
			SuperAwesome.instance.setTestMode (cTestMode);
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

		/*********************************************************************************************/
		/** SANativeInterface methods */
		/*********************************************************************************************/

		/** setter for the Ad */
		public void setAd(SAAd ad) {
			this.ad = ad;
		}
		
		public SAAd getAd() {
			return this.ad;
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
			SAVideoAd.SuperAwesomeUnitySAVideoAd(ad.placementId, ad.adJson, this.name, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var uname = this.name; 
			
			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySAVideoAd", context, ad.placementId, ad.adJson, uname, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
			}));
#else
			Debug.Log ("Open: " + this.name + ", " + ad.placementId);
#endif
		}
		
		/**
		 * This function removes the closes the fullscreen video
		 */
		public void close () {
#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAVideoAd.SuperAwesomeUnityCloseSAFullscreenVideoAd(this.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var uname = this.name;
			
			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnityCloseSAFullscreenVideoAd", context, uname);
			}));
#else 
			Debug.Log("Close: " + this.name + ", " + ad.placementId);
#endif
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



