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
		private static extern void SuperAwesomeUnitySAVideoAdPlay(int placementId,
		                                                          bool isParentalGateEnabled,
		                                                          bool shouldShowCloseButton,
		                                                          bool shouldShowSmallClickButton,
		                                                          bool shouldAutomaticallyCloseAtEnd,
		                                                          int lockOrientation);
		
		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAVideoAdHasAdAvailable(int placementId);
#endif
		
		// the video ad static instance
		private static SAVideoAd					staticInstance = null;

		// define a default callback so that it's never null and I don't have
		// to do a check every time I want to call it
		private static Action <int, SAEvent> 		callback = (p, e) => {};

		// assign default values to all of these fields
		private static bool isParentalGateEnabled 			= SuperAwesome.getInstance().defaultParentalGate ();
		private static bool shouldShowCloseButton 			= SuperAwesome.getInstance().defaultCloseButton ();
		private static bool shouldShowSmallClickButton 		= SuperAwesome.getInstance().defaultSmallClick ();
		private static bool shouldAutomaticallyCloseAtEnd 	= SuperAwesome.getInstance().defaultCloseAtEnd ();
		private static bool isTestingEnabled 				= SuperAwesome.getInstance().defaultTestMode ();
		private static bool	isBackButtonEnabled 			= SuperAwesome.getInstance().defaultBackButton ();
		private static SAOrientation orientation 			= SuperAwesome.getInstance().defaultOrientation ();
		private static SAConfiguration configuration 		= SuperAwesome.getInstance().defaultConfiguration ();

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

				var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
				var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				
				context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
					var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityVideoAd");
					saplugin.CallStatic("SuperAwesomeUnitySAVideoAdCreate", context);
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

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityVideoAd");
				saplugin.CallStatic("SuperAwesomeUnitySAVideoAdLoad", 
				                context, 
				                placementId, 
				                (int)configuration, 
				                isTestingEnabled);
			}));

#else
			Debug.Log ("SAVideoAd Load");
#endif
			
		}
		
		public static void play (int placementId) {

			// create an instrance of an SAVideoAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAVideoAd.SuperAwesomeUnitySAVideoAdPlay(placementId,
			                                         isParentalGateEnabled,
			                                         shouldShowCloseButton,
			                                         shouldShowSmallClickButton,
			                                         shouldAutomaticallyCloseAtEnd,
			                                         (int)orientation);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityVideoAd");
				saplugin.CallStatic("SuperAwesomeUnitySAVideoAdPlay", 
				                context, 
				                placementId, 
				                isParentalGateEnabled, 
				                shouldShowCloseButton, 
				                shouldShowSmallClickButton, 
				                shouldAutomaticallyCloseAtEnd, 
				                (int)orientation,
				                isBackButtonEnabled);
			}));
		
#else 
			Debug.Log ("SAVideoAd Play");
#endif
			
		}
		
		public static bool hasAdAvailable (int placementId) {

			// create an instrance of an SAVideoAd (for callbacks)
			tryAndCreateOnce ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			return SAVideoAd.SuperAwesomeUnitySAVideoAdHasAdAvailable(placementId);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityVideoAd");

			return saplugin.CallStatic<bool>("SuperAwesomeUnitySAVideoAdHasAdAvailable", context, placementId);
			

#else 
			Debug.Log ("SAVideoAd Has Ad Available");
			return false;
#endif
			
			return false;
		}
		
		////////////////////////////////////////////////////////////////////
		// Setters & getters
		////////////////////////////////////////////////////////////////////
		
		public static void setCallback (Action <int, SAEvent> value) {
			callback = value != null ? value : callback;
		}

		public static void setIsParentalGateEnabled (bool value) {
			isParentalGateEnabled = value;
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

		public static void enableCloseButton () {
			shouldShowCloseButton = true;
		}

		public static void disableCloseButton () {
			shouldShowCloseButton = false;
		}

		public static void enableSmallClickButton () {
			shouldShowSmallClickButton = true;
		}

		public static void disableSmallClickButton () {
			shouldShowSmallClickButton = false;
		}

		public static void enableCloseAtEnd () {
			shouldShowSmallClickButton = true;
		}

		public static void disableCloseAtEnd () {
			shouldAutomaticallyCloseAtEnd = false;
		}

		public static void enableBackButton () {
			isBackButtonEnabled = true;
		}
		
		public static void disableBackButton () {
			isBackButtonEnabled = false;
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

