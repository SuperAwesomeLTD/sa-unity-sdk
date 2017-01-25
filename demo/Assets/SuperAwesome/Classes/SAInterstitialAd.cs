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
		                                                                 int lockOrientation);

		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable(int placementId);
#endif

		// the interstitial ad static instance
		private static SAInterstitialAd 			staticInstance = null;

		// define a default callback so that it's never null and I don't have
		// to do a check every time I want to call it
		private static Action<int, SAEvent>			callback = (p, e) => {};

		// assign default values to all of these fields
		private static bool isParentalGateEnabled 		= SuperAwesome.instance.defaultParentalGate ();
		private static bool isTestingEnabled 			= SuperAwesome.instance.defaultTestMode ();
		private static bool isBackButtonEnabled 		= SuperAwesome.instance.defaultBackButton ();
		private static SAOrientation orientation 		= SuperAwesome.instance.defaultOrientation ();
		private static SAConfiguration configuration	= SuperAwesome.instance.defaultConfiguration ();

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

				var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				
				context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
					var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityInterstitialAd");
					saplugin.CallStatic("SuperAwesomeUnitySAInterstitialAdCreate", context);
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

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityInterstitialAd");
				saplugin.CallStatic("SuperAwesomeUnitySAInterstitialAdLoad", 
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
			                                                       (int)orientation);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityInterstitialAd");
				saplugin.CallStatic("SuperAwesomeUnitySAInterstitialAdPlay", 
				                context, 
				                placementId, 
				                isParentalGateEnabled, 
				                (int)orientation,
				                isBackButtonEnabled);
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

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityInterstitialAd");
			
			return saplugin.CallStatic<bool>("SuperAwesomeUnitySAInterstitialAdHasAdAvailable", context, placementId);

#else 
			Debug.Log ("SAInterstitialAd HasAdAvailable");
			return false;
#endif
			return false;
		}

		////////////////////////////////////////////////////////////////////
		// Setters & getters
		////////////////////////////////////////////////////////////////////

		public static void setCallback (Action<int, SAEvent> value) {
			callback = value != null ? value : callback;
		}

		public static void enableParentalGate () {
			isParentalGateEnabled = true;
		}

		public static void disableParentalGate () {
			isParentalGateEnabled = false;
		}

		public static void enableTestMode () {
			isTestingEnabled = true;
		}

		public static void disableTestMode () {
			isTestingEnabled = false;
		}

		public static void enableBackButton () {
			isBackButtonEnabled = true;
		}

		public static void disableBackButton () {
			isBackButtonEnabled = false;
		}

		public static void setConfigurationProduction () {
			configuration = SAConfiguration.PRODUCTION;
		}
		
		public static void setConfigurationStaging () {
			configuration = SAConfiguration.STAGING;
		}

		public static void setOrientationAny () {
			orientation = SAOrientation.ANY;
		}

		public static void setOrientationPortrait () {
			orientation = SAOrientation.PORTRAIT;
		}

		public static void setOrientationLandscape () {
			orientation = SAOrientation.LANDSCAPE;
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
			case "sacallback_adAlreadyLoaded": callback (placementId, SAEvent.adAlreadyLoaded); break;
			case "sacallback_adShown": callback(placementId, SAEvent.adShown); break;
			case "sacallback_adFailedToShow": callback (placementId, SAEvent.adFailedToShow); break;
			case "sacallback_adClicked": callback (placementId, SAEvent.adClicked); break;
			case "sacallback_adEnded": callback (placementId, SAEvent.adEnded); break;
			case "sacallback_adClosed": callback (placementId, SAEvent.adClosed); break;
			}
		}
	}
}
