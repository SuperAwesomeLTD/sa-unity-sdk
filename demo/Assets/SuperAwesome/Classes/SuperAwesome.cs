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
#endif

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
		private SuperAwesome (){
			// do nothing
		}

		// getters
		private string getVersion (){
			return "5.1.7";
		}
		
		private string getSdk () {
			return "unity";
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