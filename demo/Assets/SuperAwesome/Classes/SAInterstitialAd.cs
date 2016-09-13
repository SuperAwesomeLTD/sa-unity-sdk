using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace SuperAwesome {
	
	public class SAInterstitialAd: MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAInterstitialAdCreate ();


		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAInterstitialAdLoad(int placementId,
		                                                                 int configuration, 
		                                                                 bool test);

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySAInterstitialAdPlay(int placementId, 
		                                                                 bool isParentalGateEnabled,
		                                                                 bool shouldLockOrientation, 
		                                                                 int lockOrientation);

		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable(int placementId);
#endif

		// private state vars
		private static SAInterstitialAd 			staticInstance = null;
		private static bool 						isParentalGateEnabled = true;
		private static bool 						shouldLockOrientation = false;
		private static SALockOrientation 			lockOrientation = SALockOrientation.ANY;
		private static SuperAwesome.SAConfiguration configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		private static bool 						isTestingEnabled = false;
		private static Action<int, SAEvent>			callback = (p, e) => {};

		// instance constructor
		private static void tryAndCreateOnce () {
			// create just one static instance for ever!
			if (staticInstance == null) {
				GameObject obj = new GameObject ();
				staticInstance = obj.AddComponent<SAInterstitialAd> ();
				staticInstance.name = "SAInterstitialAd";
				DontDestroyOnLoad (staticInstance);
				
#if (UNITY_IPHONE && !UNITY_EDITOR) 
				SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdCreate ();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
				var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

				var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
					AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
					test.CallStatic("SuperAwesomeUnitySAInterstitialAdCreate", context);
				}));
#else 
				Debug.Log("SAInterstitialAd Create");
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

			// create an instrance of an SAInterstitialAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdLoad(placementId, 
			                                                       (int)configuration,
			                                                       isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySAInterstitialAdLoad", 
				                context, 
				                placementId, 
				                (int)configuration, 
				                isTestingEnabled);
			}));
#else
			Debug.Log ("SAInterstitialAd Load");
#endif
			
		}

		public static void play (int placementId) {

			// create an instrance of an SAInterstitialAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdPlay(placementId,
			                                                       isParentalGateEnabled,
			                                                       shouldLockOrientation,
			                                             		   (int)lockOrientation);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				test.CallStatic("SuperAwesomeUnitySAInterstitialAdPlay", 
				                context, 
				                placementId, 
				                isParentalGateEnabled, 
				                shouldLockOrientation, 
				                (int)lockOrientation);
			}));
#else
			Debug.Log ("SAInterstitialAd Play");
#endif

		}

		public static bool hasAdAvailable (int placementId) {

			// create an instrance of an SAInterstitialAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			return SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdHasAdAvailable(placementId);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

			var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAUnity");
				return test.CallStatic("SuperAwesomeUnitySAInterstitialAdHasAdAvailable", 
				                       context, 
				                       placementId);
			}));
#else 
			Debug.Log ("SAInterstitialAd HasAdAvailable");
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
		
		public static void setCallback (Action<int, SAEvent> value) {
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
