using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MiniJSON;
using System.Runtime.InteropServices;
using System;

namespace tv {
	namespace superawesome {
		namespace sdk {
			namespace publisher {

				public class AwesomeAds: MonoBehaviour {

					// the video ad static instance
					private static AwesomeAds staticInstance = null;

					// instance constructor
					private static void createInstance () {
						// create just one static instance for ever!
						if (staticInstance == null) {
							GameObject obj = new GameObject ();
							staticInstance = obj.AddComponent<AwesomeAds> ();
							staticInstance.name = "AwesomeAds";
							DontDestroyOnLoad (staticInstance);
						}
					}


#if (UNITY_IPHONE && !UNITY_EDITOR)
					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnityAwesomeAdsInit (bool loggingEnabled);
#endif

					public static void init (bool loggingEnabled) {

						createInstance ();

#if (UNITY_IPHONE && !UNITY_EDITOR)
						var loggingEnabledL = loggingEnabled;
						AwesomeAds.SuperAwesomeUnityAwesomeAdsInit (loggingEnabledL);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var loggingEnabledL = loggingEnabled;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAwesomeAds");
							var instance = saplugin.GetStatic<AndroidJavaObject>("INSTANCE");
							instance.Call("SuperAwesomeUnityAwesomeAdsInit", context, loggingEnabledL);
						}));
#else
						Debug.Log ("Initialising SDK");
#endif
					}
				}
			}
		}
	}
}

