/** imports for this class */
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This is the Unity pre-loader class that manages the Unity-iOS/Android communication
	 */
	public class SALoader: MonoBehaviour {

		/** private members */
		private int placementId = 0;

		/** delegates & events */
		public SALoaderInterface loaderDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")] 
		private static extern void SuperAwesomeUnityLoadAd2(string unityName, int placementId, bool isTestingEnabled);
#endif

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

		/** This function is used as a static wrapper against SALoaderScript's member loadAd function */
		public void loadAd(int placementId) {
			/** assign placement */
			this.placementId = placementId;
			
			/** get if testing is enabled */
			bool isTestingEnabled = SuperAwesome.instance.isTestingEnabled ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SALoader.SuperAwesomeUnityLoadAd2(this.name, placementId, isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			Debug.Log("Not in Android yet");
#else
			Debug.Log ("Load: " + this.name + ", " + placementId + ", " + isTestingEnabled);
#endif
		}

		/** <Functions that are called by "UnitySendMesage" from the iOS / Android plugin */
		public void nativeCallback_LoadSuccess (string adJson) {

			if (loaderDelegate != null) {
				/** create a new ad object */
				SAAd ad = new SAAd();
				
				/** and just assign it the adJson body and placement Id */
				ad.adJson = adJson;
				ad.placementId = this.placementId;
				
				/** then call the callback-function */
				loaderDelegate.didLoadAd(ad);
			}
		}
		
		public void nativeCallback_LoadError (string placementId) {
			if (loaderDelegate != null) {
				/** parse the placement id */
				this.placementId = int.Parse(placementId);
				
				/** call the callback-function */
				loaderDelegate.didFailToLoadAd(this.placementId);
			}
		}
	}
}