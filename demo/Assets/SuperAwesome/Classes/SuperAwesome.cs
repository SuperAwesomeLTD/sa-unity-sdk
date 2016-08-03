/** 
 * Imports used for this class
 */
using UnityEngine;
using System.Collections;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	/**
	 * This is a Singleton class through which SDK users setup their AwesomeAds instance
	 */
	public class SuperAwesome {

		/** constants */
		private const int CONFIGURATION_PRODUCTION = 0;
		private const int CONFIGURATION_STAGING = 1;

		/** other variables */
		private string baseUrl;
		private bool isTestEnabled;
		private int config;

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
			/** log current version and sdk type */
			Debug.Log (getSdkVersion ());
			/** prepare to rock! */
			this.setConfigurationProduction ();
			this.disableTestMode ();
		}

		// setters

		public void setConfiguration(int configuration) {
			if (configuration == CONFIGURATION_PRODUCTION) {
				setConfigurationProduction ();
			} else {
				setConfigurationStaging ();
			}
		}
		
		public void setConfigurationProduction() {
			this.config = CONFIGURATION_PRODUCTION;
		}
		
		public void setConfigurationStaging() {
			this.config = CONFIGURATION_STAGING;
		}
		
		public void enableTestMode() {
			this.isTestEnabled = true;
		}
		
		public void disableTestMode() {
			this.isTestEnabled = false;
		}
		
		public void setTestMode(bool testMode) {
			this.isTestEnabled = testMode;
		}

		// getters
		private string getVersion(){
			return "3.1.7";
		}
		
		private string getSdk() {
			return "unity";
		}
		
		public string getSdkVersion() {
			return getSdk () + "_" + getVersion ();
		}

		public int getConfiguration() {
			return this.config;
		}

		public bool isTestingEnabled() {
			return this.isTestEnabled;
		}
	}
}