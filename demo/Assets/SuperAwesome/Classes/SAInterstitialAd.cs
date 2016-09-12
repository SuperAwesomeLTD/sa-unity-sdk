using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;

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
		private static extern void SuperAwesomeUnitySAInterstitialAdPlay(bool isParentalGateEnabled,
		                                                                 bool shouldLockOrientation, 
		                                                                 int lockOrientation);

		[DllImport ("__Internal")]
		private static extern bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable();
#endif

		// private state vars
		private static SAInterstitialAd 			staticInstance = null;
		private static bool 						isParentalGateEnabled = true;
		private static bool 						shouldLockOrientation = false;
		private static SALockOrientation 			lockOrientation = SALockOrientation.ANY;
		private static SuperAwesome.SAConfiguration configuration = SuperAwesome.SAConfiguration.PRODUCTION;
		private static bool 						isTestingEnabled = false;
		private static SAInterface 					listener;

		// constructor
		public SAInterstitialAd () {
			this.name = "SAInterstitialAd";
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

			// create just one static instance for ever!
			if (staticInstance == null) {
				GameObject obj = new GameObject ();
				staticInstance = obj.AddComponent<SAInterstitialAd> ();
				staticInstance.name = "SAInterstitialAd";
				DontDestroyOnLoad (staticInstance);

#if (UNITY_IPHONE && !UNITY_EDITOR) 
				SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdCreate ();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
				// do nothing
#endif

			}

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdLoad(placementId, 
			                                                       (int)configuration,
			                                                       isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			// do nothing
#endif

		}

		public static void play () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdPlay(isParentalGateEnabled,
			                                                       shouldLockOrientation,
			                                             		   (int)lockOrientation);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			// do nothing
#endif

		}

		public static bool hasAdAvailable () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SAInterstitialAd.SuperAwesomeUnitySAInterstitialAdHasAdAvailable();
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			// do nothing
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
		
		public static void setListener (SAInterface value) {
			listener = value;
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
			case "sacallback_adLoaded":{
				if (listener != null) listener.SAAdLoaded (placementId); break;
			}
			case "sacallback_adFailedToLoad":{
				if (listener != null) listener.SAAdFailedToLoad (placementId); break;
			}
			case "sacallback_adShown":{
				if (listener != null) listener.SAAdShown (); break;
			}
			case "sacallback_adFailedToShow":{
				if (listener != null) listener.SAAdFailedToShow (); break;
			}
			case "sacallback_adClicked":{
				if (listener != null) listener.SAAdClicked (); break;
			}
			case "sacallback_adClosed":{
				if (listener != null) listener.SAAdClosed (); break;
			}
			}
		}
	}
}
