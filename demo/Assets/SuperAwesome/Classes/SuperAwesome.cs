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
		private static extern void SuperAwesomeUnitySuperAwesomeSetVersion (string version, string sdk);

#endif

		// local cpi module 
		private SACPI sacpi = null;

		// sdk & version
		private const string version = "5.3.0";
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

			// create the cpi module
			if (sacpi == null) {
				GameObject obj = new GameObject ();
				sacpi = obj.AddComponent<SACPI> ();
				sacpi.name = "SAUnityCPI";
				// DontDestroyOnLoad (sacpi);
			}

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
			sacpi.handleCPI (value);
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
	}
}
