/** 
 * Imports used for this class
 */
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This is the Unity pre-loader class that manages the Unity-iOS/Android communication
	 */
	public class SALoader: MonoBehaviour, SANativeInterface {

		/** instance index */
		private static uint index = 0;

		/** private members */
		private int placementId = 0;

		/** delegates & events */
		public SALoaderInterface loaderDelegate = null;

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")] 
		private static extern void SuperAwesomeUnityLoadAd(string unityName, int placementId, bool isTestingEnabled, int config);
#endif

		/** static function initialiser */
		public static SALoader createInstance() {
			/** create a new game object */
			GameObject obj = new GameObject ();
			
			/** add to that new object the video ad */
			SALoader loader = obj.AddComponent<SALoader> ();
			loader.name = "SALoader_" + (++SALoader.index);
			
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
			int config = (int)SuperAwesome.instance.getConfiguration ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SALoader.SuperAwesomeUnityLoadAd(this.name, placementId, isTestingEnabled, config);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var uname = this.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnityLoadAd", context, uname, placementId, isTestingEnabled, config);
			}));
#else
			Debug.Log ("Load: " + this.name + ", " + placementId + ", " + isTestingEnabled);
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
				if (loaderDelegate != null) {
					loaderDelegate.didFailToLoadAd(this.placementId);
				}
				return;
			}

			switch (type) {
			case "callback_didLoadAd":{
				/** form the new ad */
				Dictionary<string, object> data = payloadDict["adJson"] as Dictionary<string, object>;
				SAAd ad = new SAAd();
				ad.adJson = Json.Serialize(data);
				ad.placementId = this.placementId;

				if (loaderDelegate != null) {
					loaderDelegate.didLoadAd(ad);
				}
				break;
			}
			case "callback_didFailToLoadAd":{
				if (loaderDelegate != null) {
					loaderDelegate.didFailToLoadAd(this.placementId);
				}
				break;
			}
			}
		}
	}
}