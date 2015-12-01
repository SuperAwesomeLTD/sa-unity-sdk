using UnityEngine;
using System.Collections;

namespace SuperAwesome
{
	public class SuperAwesome {

		private string baseURL = "https://ads.superawesome.tv/v2";

		private static SuperAwesome _instance;

		public static SuperAwesome instance {
			get {
				if(_instance == null){
					_instance = new SuperAwesome();
				}
				return _instance;
			}
		}

		public AdManager adManager { 
			get; private set; 
		}

		public SuperAwesome(){
			Debug.Log (SuperAwesome.getSdkVersion ());
			adManager = new AdManager (this.baseURL);
		}

		private static string getVersion(){
			return "2.2.3";
		}

		private static string getSdk() {
			return "unity";
		}

		public static string getSdkVersion() {
			return SuperAwesome.getSdk () + "_" + SuperAwesome.getVersion ();
		}

		public string getBaseURL() {
			return this.baseURL;
		}
	}
}