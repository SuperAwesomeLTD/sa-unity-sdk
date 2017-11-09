using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace tv {
	namespace superawesome {
		namespace sdk {
			namespace publisher {

				public class SAVersion {
				
#if (UNITY_IPHONE && !UNITY_EDITOR)

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnityVersionSetVersion (string version, string sdk);

#endif

					// sdk & version
					private static string version = "6.1.5";
					private static string sdk = "unity";

					// getters
					public static void setVersionInNative () {

#if (UNITY_IPHONE && !UNITY_EDITOR)
						SAVersion.SuperAwesomeUnityVersionSetVersion (version, sdk);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var versionL = version;
						var sdkL = sdk;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityVersion");
						saplugin.CallStatic("SuperAwesomeUnityVersionSetVersion", context, versionL, sdkL);
						}));

#else
						Debug.Log ("Set Sdk version to " + getSdkVersion());
#endif
					}

					private static string getVersion (){
						return version;
					}

					private static string getSdk () {
						return sdk;
					}

					public static string getSdkVersion () {
						return getSdk () + "_" + getVersion ();
					}
				}
			}
		}
	}
}



