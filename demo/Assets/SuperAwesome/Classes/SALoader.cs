/** imports for this class */
using UnityEngine;
using System;
using System.Collections;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This class is a normal (non MonoBehaviour, non Scriptable) C# class that acts as
	 * a wrapper for the SALoaderScript class.
	 */
	public class SALoader {

		/** the loader script instance */
		private GameObject gameObject;
		private SALoaderInternal loader;

		/** instance variable, since SuperAwesome is a singleton */
		private static SALoader _instance;
		public static SALoader instance {
			get {
				if(_instance == null){
					_instance = new SALoader();
				}
				return _instance;
			}
		}

		/** constructor to create the loader */
		private SALoader() {
			gameObject = new GameObject ();
			loader = gameObject.AddComponent<SALoaderInternal> ();
			loader.name = "loadAd_" + (new System.Random()).Next(100, 1000).ToString();
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
			loader.loadAd (placementId, success, error);
		}

		/**
		 * The private SALoaderInternal class, derived from Unity's MonoBehaviour,
		 * that actually does all the heavy lifting to communicate with iOS / Android.
		 * We need this class in order to be able to add it to a GameObject later on
		 * and be registerd as part of Unity's lifecycle
		 */ 
		private class SALoaderInternal : MonoBehaviour {
			
			/** private members */
			Func<SAAd, int> successCallback = null;
			Func<int, int> errorCallback = null;
			
			/** local placement Id */
			private int placementId = 0;
			
			/** Init the script name */
			void Start () {
				/** do nothing */
			}
			
			/** Update is called once per frame */
			void Update () {
				/** do nothing */
			}
			
			/**
			 * This function will load ad data by calling the specific Android or iOS SABridge function
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
}