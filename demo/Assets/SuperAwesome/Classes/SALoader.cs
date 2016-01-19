/** imports for this class */
using UnityEngine;
using System;
using System.Collections;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This is the Unity preloader class
	 */
	public class SALoader: MonoBehaviour {

		/** private members */
		Func<SAAd, int> successCallback = null;
		Func<int, int> errorCallback = null;
		private int placementId = 0;

		/** static function initialiser */
		public static SALoader createInstance() {
			/** create a new game object */
			GameObject obj = new GameObject ();
			
			/** add to that new object the video ad */
			SALoader loader = obj.AddComponent<SALoader> ();
			loader.name = "SALoader_" + (new System.Random()).Next(100, 1000).ToString();
			
			/** and return the ad Obj instance */
			return loader;
		}

		void Start () {
			/** do nothing */
		}
		
		void Update () {
			/** do nothing */
		}

		/**
		 * This function is used as a static wrapper against SALoaderScript's member 
		 * loadAd function
		 * 
		 * @param: placementID - the SA placement id that you want to load an ad for
		 * @param: success - a callback for when the ad gets loaded, with SAAd as "returning param"
		 * @param: error - a callback for when the ad fails to load, with an int as "returning param"
		 */
		public void loadAd(int placementId, Func<SAAd, int> success, Func<int, int> error) {
			/** assign callbacks */
			this.successCallback = success;
			this.errorCallback = error;
			this.placementId = placementId;
			
			/** get if testing is enabled */
			bool isTestingEnabled = SuperAwesome.instance.isTestingEnabled ();
			
			/** call the SABridge function for iOS / Android */
			#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
			SABridge.loadAd(this.name, placementId, isTestingEnabled);
			#else
			Debug.Log ("Load: " + this.name + ", " + placementId + ", " + isTestingEnabled);
			#endif
		}

		/**
		  * This func is called externally from iOS / Android by a "UnitySendMessage" call
		  */
		public void loadAdSuccessFunc (string adJson) {
			if (successCallback != null) {
				/** create a new ad object */
				SAAd ad = new SAAd();
				
				/** and just assign it the adJson body and placement Id */
				ad.adJson = adJson;
				ad.placementId = this.placementId;
				
				/** then call the callback-function */
				successCallback(ad);
			}
		}
		
		/**
	     * This func is called externally from iOS / Android by a "UnitySendMessage" call
		 */
		public void loadAdErrorFunc (string placementId) {
			if (errorCallback != null) {
				/** parse the placement id */
				this.placementId = int.Parse(placementId);
				
				/** call the callback-function */
				errorCallback(this.placementId);
			}
		}
	}
}