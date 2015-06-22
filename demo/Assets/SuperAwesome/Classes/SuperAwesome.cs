using UnityEngine;
using System.Collections;

namespace SuperAwesome
{
	public class SuperAwesome {

		private static SuperAwesome _instance;

		public static SuperAwesome instance
		{
			get
			{
				if(_instance == null){
					_instance = new SuperAwesome();
				}
				return _instance;
			}
		}

		public AdManager adManager { get; private set; }

		public SuperAwesome(){
			Debug.Log (SuperAwesome.getVersion ());
			adManager = new AdManager ("https://beta.ads.superawesome.tv/v2");
		}

		public static string getVersion(){
			return "SuperAwesome Unity SDK version 2.0";
		}
	}
}