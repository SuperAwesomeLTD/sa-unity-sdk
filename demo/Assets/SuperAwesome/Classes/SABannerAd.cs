using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace SuperAwesome {

	public class SABannerAd: MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySABannerAdCreate(string unityName);

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySABannerAdLoad(string unityName,
		                                                           int placementId,
		                                                           int configuration, 
		                                                           bool test);

		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySABannerAdHasAdAvailable(string unityName);
		

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySABannerAdPlay(string unityName,
		                                                           bool isParentalGateEnabled, 
		                                                           int position, 
		                                                           int size, 
		                                                           int color);

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySABannerAdClose(string unityName);

#endif

		// enums for different banner specific properties
		public enum BannerPosition {
			TOP = 0,
			BOTTOM = 1
		}
		public enum BannerSize {
			BANNER_320_50 = 0,
			BANNER_300_50 = 1,
			BANNER_728_90 = 2,
			BANNER_300_250 = 3
		}
		public enum BannerColor {
			BANNER_TRANSPARENT = 0,
			BANNER_GRAY = 1
		}

		// banner index
		private static uint 					index = 0;

		// private state vars
		private bool 							isParentalGateEnabled = true;
		private BannerPosition 					position = BannerPosition.BOTTOM;
		private BannerSize 						size = BannerSize.BANNER_320_50;
		private BannerColor 					color = BannerColor.BANNER_GRAY;
		private SuperAwesome.SAConfiguration 	configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		private bool 							isTestingEnabled = false;
		private Action <int, SAEvent>	    	callback = (p, e) => {};

		// create method
		public static SABannerAd createInstance() {
			GameObject obj = new GameObject ();
			SABannerAd adObj = obj.AddComponent<SABannerAd> ();
			adObj.name = "SABannerAd_" + (++SABannerAd.index);
			DontDestroyOnLoad (obj);

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SABannerAd.SuperAwesomeUnitySABannerAdCreate(adObj.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			string nameL = adObj.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySABannerAdCreate", context, nameL);
			}));
#else 
			Debug.Log (adObj.name + " Create");
#endif
			
			return adObj;
		}

		// start method for MonoObject
		void Start () {
			if (this.GetComponent<Image> () != null) {
				Color current = this.GetComponent<Image>().color;
				current.a = 0;
				this.GetComponent<Image>().color = current;
			}
		}

		// update method for MonoObject
		void Update () {
			// do nothing
		}

		////////////////////////////////////////////////////////////////////
		// Banner specific method
		////////////////////////////////////////////////////////////////////

		public void load (int placementId) {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SABannerAd.SuperAwesomeUnitySABannerAdLoad(this.name,
			                                           placementId, 
			                                           (int)configuration,
			                                           isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			string nameL = this.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySABannerAdLoad", context, nameL, placementId, (int)configuration, isTestingEnabled);
			}));
#else
			Debug.Log (this.name + " Load");
#endif
		
		}

		public void play () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SABannerAd.SuperAwesomeUnitySABannerAdPlay(this.name,
			                                           isParentalGateEnabled,
			                                           (int)position,
			                                           (int)size,
			                                           (int)color);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			string nameL = this.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySABannerAdPlay", context, nameL, isParentalGateEnabled, (int)position, (int)size, (int)color);
			}));
#else 
			Debug.Log (this.name + " Play");
#endif

		}

		public bool hasAdAvailable () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			return SABannerAd.SuperAwesomeUnitySABannerAdHasAdAvailable(this.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var nameL = this.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				return test.CallStatic("SuperAwesomeUnitySABannerAdHasAdAvailabke", context, nameL);
			}));
#else 
			Debug.Log (this.name + " HasAdAvailable");
#endif
			return false;

		}

		public void close () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SABannerAd.SuperAwesomeUnitySABannerAdClose(this.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");
			var nameL = this.name;

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				return test.CallStatic("SuperAwesomeUnitySABannerAdClose", context, nameL);
			}));
#else 
			Debug.Log (this.name + " Close");
#endif 
		}

		////////////////////////////////////////////////////////////////////
		// Setters & getters
		////////////////////////////////////////////////////////////////////

		public void setCallback (Action<int, SAEvent> value) {
			this.callback = value != null ? value : this.callback;
		}

		public void setIsParentalGateEnabled (bool value) {
			isParentalGateEnabled = value;
		}

		public void setPosition (BannerPosition value) {
			position = value;
		}

		public void setSize (BannerSize value) {
			size = value;
		}

		public void setColor (BannerColor value) {
			color = value;
		}

		public void setTest (bool value) {
			isTestingEnabled = value;
		}

		public void setTestEnabled () {
			isTestingEnabled = true;
		}

		public void setTestDisabled () {
			isTestingEnabled = false;
		}

		public void setConfiguration (SuperAwesome.SAConfiguration value) {
			configuration = value;
		}

		public void setConfigurationProduction () {
			configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		}

		public void setConfigurationStaging () {
			configuration = SuperAwesome.SAConfiguration.STAGING;
		}

		public bool getIsParentalGateEnabled () {
			return isParentalGateEnabled;
		}
		
		public BannerPosition getPosition () {
			return position;
		}
		
		public BannerSize getSize () {
			return size;
		}
		
		public BannerColor getColor () {
			return color;
		}

		////////////////////////////////////////////////////////////////////
		// Native callbacks
		////////////////////////////////////////////////////////////////////

		public void nativeCallback(string payload) {
			Dictionary<string, object> payloadDict;
			string type = "";
			int placementId = 0;

			// try to get payload and type data
			try {
				payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
				type = (string) payloadDict["type"];
				string plac = (string) payloadDict["placementId"];
				int.TryParse(plac, out placementId);
			} catch {
				Debug.Log ("Error w/ callback!");
				return;
			}
			
			switch (type) {
			case "sacallback_adLoaded":  callback(placementId, SAEvent.adLoaded); break;
			case "sacallback_adFailedToLoad": callback(placementId, SAEvent.adFailedToLoad); break;
			case "sacallback_adShown": callback(placementId, SAEvent.adShown); break;
			case "sacallback_adFailedToShow": callback (placementId, SAEvent.adFailedToShow); break;
			case "sacallback_adClicked": callback (placementId, SAEvent.adClicked); break;
			case "sacallback_adClosed": callback (placementId, SAEvent.adClosed); break;
			}
		}
	}
}
