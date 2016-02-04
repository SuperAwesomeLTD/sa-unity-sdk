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
		private const string BASE_URL_STAGING = "https://ads.staging.superawesome.tv/v2";
		private const string BASE_URL_DEVELOPMENT = "https://ads.dev.superawesome.tv/v2";
		private const string BASE_URL_PRODUCTION = "https://ads.superawesome.tv/v2";

		/** other variables */
		private string baseUrl;
		private bool isTestEnabled;

		public enum SAConfiguration{
			STAGING = 0,
			DEVELOPMENT = 1,
			PRODUCTION = 2
		}
		private SAConfiguration config = SAConfiguration.PRODUCTION;

		/** instance variable, since SuperAwesome is a singleton */
		private static SuperAwesome _instance;
		public static SuperAwesome instance {
			get {
				if(_instance == null){
					_instance = new SuperAwesome();
				}
				return _instance;
			}
		}

		/** public constructor */
		private SuperAwesome(){
			/** log current version and sdk type */
			Debug.Log (getSdkVersion ());
			/** prepare to rock! */
			this.setConfigurationProduction ();
			this.disableTestMode ();
		}

		/** functions to get info about the current SDK */
		private static string getVersion(){
			return "3.0.1";
		}

		private static string getSdk() {
			return "unity";
		}

		public static string getSdkVersion() {
			return SuperAwesome.getSdk () + "_" + SuperAwesome.getVersion ();
		}

		/** group of functions that encapsulate config / URL functionality */
		public void setConfigurationProduction() {
			this.config = SAConfiguration.PRODUCTION;
			this.baseUrl = BASE_URL_PRODUCTION;
		}

		public void setConfigurationStaging() {
			this.config = SAConfiguration.STAGING;
			this.baseUrl = BASE_URL_STAGING;
		}

		public void setConfigurationDevelopment() {
			this.config = SAConfiguration.DEVELOPMENT;
			this.baseUrl = BASE_URL_DEVELOPMENT;
		}

		public string getBaseURL() {
			return this.baseUrl;
		}

		public SAConfiguration getConfiguration() {
			return this.config;
		}

		/** functions that encapsulate test functionality */
		public void enableTestMode() {
			this.isTestEnabled = true;
		}

		public void disableTestMode() {
			this.isTestEnabled = false;
		}

		public bool isTestingEnabled() {
			return this.isTestEnabled;
		}
	}

}