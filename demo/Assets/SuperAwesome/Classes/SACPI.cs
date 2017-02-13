using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

/** part for the SuperAwesome namespace */
namespace SuperAwesome {

	// main class
	public class SACPI : MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)
		[DllImport ("__Internal")]
		private static extern void SuperAwesomeUnitySACPIHandleCPI ();
#endif

		// define a default callback so that it's never null and I don't have
		// to do a check every time I want to call it
		private static Action<bool>	installCallback = (p) => {};

		// a private instance
		private static SACPI sharedInstance = null;

		public static SACPI getInstance () {

			if (sharedInstance == null) {
				GameObject obj = new GameObject ();
				sharedInstance = obj.AddComponent<SACPI> ();
				sharedInstance.name = "SAUnityCPI";
				DontDestroyOnLoad (sharedInstance);
			}

			return sharedInstance;
		
		}

		public void handleInstall (Action<bool> value) {
			// get the callback
			installCallback = value != null ? value : installCallback;

#if (UNITY_IPHONE && !UNITY_EDITOR)

			SACPI.SuperAwesomeUnitySACPIHandleCPI ();

#elif (UNITY_ANDROID && !UNITY_EDITOR)

			var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
			var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

			context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.unity.SAUnityCPI");
			saplugin.CallStatic("SuperAwesomeUnitySACPIHandleCPI", context);
			}));

#else
			Debug.Log ("Handle CPI");
#endif
		}

		// MonoDevelop start implementation
		void Start (){
			// do nothing
		}

		// MonoDevelop update implementation
		void Update () {
			// do nothing
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
			case "sacallback_HandleCPI": installCallback (success); break;
			}

		}
	}
}