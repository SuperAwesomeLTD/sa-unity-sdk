/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	// main singleton class
	public class SuperAwesome {

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySuperAwesomeHandleCPI ();

		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySetVersion (string version, string sdk);

#endif

		// sdk & version
		private const string version = "5.1.7";
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
			SuperAwesome.SuperAwesomeUnitySetVersion (version, sdk);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
			
			var versionL = version;
			var sdkL = sdk;
			
			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
			
			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityVersion");
				saplugin.CallStatic("SuperAwesomeUnitySetVersion", context, versionL, sdkL);
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

		public void handleCPI () {

#if (UNITY_IPHONE && !UNITY_EDITOR)

			SuperAwesome.SuperAwesomeUnitySuperAwesomeHandleCPI ();

#elif (UNITY_ANDROID && !UNITY_EDITOR)
			// do nothing
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
	}
}