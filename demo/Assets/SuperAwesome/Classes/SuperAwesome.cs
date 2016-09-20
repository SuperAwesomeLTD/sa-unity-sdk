/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;

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
			return "5.0.1";
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
	}
}