/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	// main singleton class
	public class SuperAwesome {

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
		private SuperAwesome(){
			// do nothing
		}

		// getters
		private string getVersion(){
			return "5.0.0";
		}
		
		private string getSdk() {
			return "unity";
		}
		
		public string getSdkVersion() {
			return getSdk () + "_" + getVersion ();
		}
	}
}