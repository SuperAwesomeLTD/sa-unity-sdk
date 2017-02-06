/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	// main singleton class
	public class SuperAwesome {

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySuperAwesomeHandleCPI ();

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySuperAwesomeSetVersion (string version, string sdk);

#endif

		// define a default callback so that it's never null and I don't have
		// to do a check every time I want to call it
		private static Action<bool>	cpiCallback = (p) => {};
		private static Action<bool> cpiStagingCallback = (p) => {};


		// sdk & version
		private const string version = "5.2.2";
		private const string sdk = "unity";

		// Singleton stuff
		private static SuperAwesome _instance;
		public static SuperAwesome instance {
			get {
				if(_instance == null){
					_instance = new SuperAwesome();
				}
				return _instance;
			}
		}

		// constructor
		private SuperAwesome () {
#if (UNITY_IPHONE && !UNITY_EDITOR) 
			SuperAwesome.SuperAwesomeUnitySuperAwesomeSetVersion (version, sdk);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			
			var versionL = version;
			var sdkL = sdk;
			
			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnitySuperAwesome");
				saplugin.CallStatic("SuperAwesomeUnitySuperAwesomeSetVersion", context, versionL, sdkL);
			}));
			
#else 
			Debug.Log ("Set Sdk version to " + getSdkVersion());
#endif
		}

		// getters
		private string getVersion (){
			return version;
		}
		
		private string getSdk () {
			return sdk;
		}
		
		public string getSdkVersion () {
			return getSdk () + "_" + getVersion ();
		}

		public void handleCPI (Action<bool> value) {
			// get the callback
			cpiCallback = value != null ? value : cpiCallback;

#if (UNITY_IPHONE && !UNITY_EDITOR)

			SuperAwesome.SuperAwesomeUnitySuperAwesomeHandleCPI ();

#elif (UNITY_ANDROID && !UNITY_EDITOR)

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnitySuperAwesome");
				saplugin.CallStatic("SuperAwesomeUnitySuperAwesomeHandleCPI", context);
			}));

#else
			Debug.Log ("Handle CPI");
#endif
		}

		// default state vars
		public int defaultPlacementId () {
			return 0;
		}

		public bool defaultTestMode () {
			return false;
		}

		public bool defaultParentalGate () {
			return true;
		}

		public SAConfiguration defaultConfiguration () {
			return SAConfiguration.PRODUCTION;
		}

		public SAOrientation defaultOrientation () {
			return SAOrientation.ANY;
		}
	
		public bool defaultCloseButton () {
			return false;
		}

		public bool defaultSmallClick () {
			return false;
		}

		public bool defaultCloseAtEnd () {
			return true;
		}

		public bool defaultBgColor () {
			return false;
		}

		public bool defaultBackButton () {
			return false;
		}

		public SABannerPosition defaultBannerPosition () {
			return SABannerPosition.BOTTOM;
		}

		public int defaultBannerWidth () {
			return 320;
		}

		public int defaultBannerHeight () {
			return 50;
		}

		////////////////////////////////////////////////////////////////////
		// Native callbacks
		////////////////////////////////////////////////////////////////////
		
		public void nativeCallback(string payload) {
			Dictionary<string, object> payloadDict;
			string type = "";
			bool success = false;
			
			// try to get payload and type data
			try {
				payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
				type = (string) payloadDict["type"];
				string suc = (string) payloadDict["success"];
				bool.TryParse(suc, out success);
			} catch {
				Debug.Log ("Error w/ callback!");
				return;
			}

			switch (type) {
			case "sacallback_HandleCPI": cpiCallback (success); break;
			}

		}
	}
}
