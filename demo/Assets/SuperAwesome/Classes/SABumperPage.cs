using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace tv {
	namespace superawesome {
		namespace sdk {
			namespace publisher {

				public class SABumperPage : MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnityBumperOverrideName (string name);

#endif

					// the interstitial ad static instance
					private static SABumperPage 			staticInstance = null;

					// instance constructor
					public static void createInstance () {
						// create just one static instance for ever!
						if (staticInstance == null) {
							GameObject obj = new GameObject ();
							staticInstance = obj.AddComponent<SABumperPage> ();
							staticInstance.name = "SABumperPage";
							DontDestroyOnLoad (staticInstance);
						}
					}

					public static void overrideName (string name) {
						createInstance ();

#if (UNITY_IPHONE && !UNITY_EDITOR)
						SABumperPage.SuperAwesomeUnityBumperOverrideName (name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var nameL = name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBumperPage");
							saplugin.CallStatic("SuperAwesomeUnityBumperOverrideName", context, nameL);
						}));

#else
						Debug.Log ("Trying to set name to " + name);
#endif
					}
				}
			}
		}
	}
}


