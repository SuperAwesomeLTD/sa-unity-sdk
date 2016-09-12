using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace SuperAwesome {
	
	public class SAVideoAd: MonoBehaviour {
		
#if (UNITY_IPHONE && !UNITY_EDITOR)

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAVideoAdCreate ();

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAVideoAdLoad(int placementId,
		                                                       	  int configuration, 
		                                                          bool test);
		
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAVideoAdPlay(bool isParentalGateEnabled,
		                                                          bool shouldShowCloseButton,
		                                                          bool shouldShowSmallClickButton,
		                                                          bool shouldAutomaticallyCloseAtEnd,
		                                                          bool shouldLockOrientation, 
		                                                          int lockOrientation);
		
		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAVideoAdHasAdAvailable();
#endif
		
		// private state vars
		private static SAVideoAd					staticInstance = null;
		private static bool 						isParentalGateEnabled = true;
		private static bool 						shouldLockOrientation = false;
		private static SALockOrientation 			lockOrientation = SALockOrientation.ANY;
		private static bool 						shouldShowCloseButton = true;
		private static bool 						shouldShowSmallClickButton = false;
		private static bool 						shouldAutomaticallyCloseAtEnd = false;
		private static SuperAwesome.SAConfiguration	configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		private static bool 						isTestingEnabled = false;
		private static Action <int, SAEvent> 		callback = (p, e) => {};

		// instance constructor
		private static void tryAndCreateOnce () {
			// create just one static instance for ever!
			if (staticInstance == null) {
				GameObject obj = new GameObject ();
				staticInstance = obj.AddComponent<SAVideoAd> ();
				staticInstance.name = "SAVideoAd";
				DontDestroyOnLoad (staticInstance);
				
#if (UNITY_IPHONE && !UNITY_EDITOR) 
				SAVideoAd.SuperAwesomeUnitySAVideoAdCreate ();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
				var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

				var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
					AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
					test.CallStatic("SuperAwesomeUnitySAVideoAdCreate", context);
				}));
#else 
				Debug.Log ("SAVideoAd Create");
#endif
			}
		}

		// MonoDevelop start implementation
		void Start (){
			if (this.GetComponent<Image> () != null) {
				Color current = this.GetComponent<Image>().color;
				current.a = 0;
				this.GetComponent<Image>().color = current;
			}
		}
		
		// MonoDevelop update implementation
		void Update () {
			// do nothing
		}
		
		////////////////////////////////////////////////////////////////////
		// Interstitial specific method
		////////////////////////////////////////////////////////////////////
		
		public static void load (int placementId) {

			// create an instrance of an SAVideoAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAVideoAd.SuperAwesomeUnitySAVideoAdLoad(placementId, 
			                                         (int)configuration,
			                                         isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySAVideoAdLoad", context, placementId, (int)configuration, isTestingEnabled);
			}));
#else
			Debug.Log ("SAVideoAd Load");
#endif
			
		}
		
		public static void play () {

			// create an instrance of an SAVideoAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAVideoAd.SuperAwesomeUnitySAVideoAdPlay(isParentalGateEnabled,
			                                         shouldShowCloseButton,
			                                         shouldShowSmallClickButton,
			                                         shouldAutomaticallyCloseAtEnd,
			                                         shouldLockOrientation,
			                                         (int)lockOrientation);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySAVideoAdPlay", context, isParentalGateEnabled, shouldShowCloseButton, shouldShowSmallClickButton, shouldAutomaticallyCloseAtEnd, shouldLockOrientation, (int)lockOrientation);
			}));
#else 
			Debug.Log ("SAVideoAd Play");
#endif
			
		}
		
		public static bool hasAdAvailable () {

			// create an instrance of an SAVideoAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			return SAVideoAd.SuperAwesomeUnitySAVideoAdHasAdAvailable();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				return test.CallStatic("SuperAwesomeUnitySAVideoAdHasAdAvailable", context);
			}));
#else 
			Debug.Log ("SAVideoAd Has Ad Available");
			return false;
#endif
			
			return false;
		}
		
		////////////////////////////////////////////////////////////////////
		// Setters & getters
		////////////////////////////////////////////////////////////////////
		
		public static void setIsParentalGateEnabled (bool value) {
			isParentalGateEnabled = value;
		}
		
		public static void setShouldLockOrientation (bool value) {
			shouldLockOrientation = value;
		}
		
		public static void setLockOrientation (SALockOrientation value) {
			lockOrientation = value;
		}

		public static void setShouldShowCloseButton (bool value) {
			shouldShowCloseButton = value;
		}

		public static void setShouldShowSmallClickButton (bool value) {
			shouldShowSmallClickButton = value;
		}

		public static void setShouldAutomaticallyCloseAtEnd (bool value) {
			shouldAutomaticallyCloseAtEnd = value;	
		}
		
		public static void setTest (bool value) {
			isTestingEnabled = value;
		}
		
		public static void setTestEnabled () {
			isTestingEnabled = true;
		}
		
		public static void setTestDisabled () {
			isTestingEnabled = false;
		}
		
		public static void setConfiguration (SuperAwesome.SAConfiguration value) {
			configuration = value;
		}
		
		public static void setConfigurationProduction () {
			configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		}
		
		public static void setConfigurationStaging () {
			configuration = SuperAwesome.SAConfiguration.STAGING;
		}
		
		public static void setCallback (Action <int, SAEvent> value) {
			callback = value != null ? value : callback;
		}
		
		public static bool getIsParentalGateEnabled () {
			return isParentalGateEnabled;
		}
		
		public static bool getShouldLockOrientation () {
			return shouldLockOrientation;
		}
		
		public static SALockOrientation getLockOrientation () {
			return lockOrientation;
		}

		public static bool getShouldShowCloseButton () {
			return shouldShowCloseButton;
		}
		
		public static bool getShouldShowSmallClickButton () {
			return shouldShowSmallClickButton;
		}
		
		public static bool getShouldAutomaticallyCloseAtEnd (bool value) {
			return shouldAutomaticallyCloseAtEnd;	
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

