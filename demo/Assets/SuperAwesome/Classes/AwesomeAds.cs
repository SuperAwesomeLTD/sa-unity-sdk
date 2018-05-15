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

					// define a default callback so that it's never null and I don't have
					// to do a check every time I want to call it
					private static Action <GetIsMinorModel> 		callback = (model) => {};

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

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnityAwesomeAdsTriggerAgeCheck (string age);
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
							saplugin.CallStatic("SuperAwesomeUnityAwesomeAdsInit", context, loggingEnabledL);
						}));
#else
						Debug.Log ("Initialising SDK");
#endif
					}

					public static void triggerAgeCheck (string age, Action<GetIsMinorModel> value) {

						createInstance ();

						callback = value;

#if (UNITY_IPHONE && !UNITY_EDITOR)
						var ageL = age;
						AwesomeAds.SuperAwesomeUnityAwesomeAdsTriggerAgeCheck (ageL);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var ageL = age;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAwesomeAds");
							saplugin.CallStatic("SuperAwesomeUnityAwesomeAdsTriggerAgeCheck", context, ageL);
						}));
#else
						Debug.Log ("triggerAgeCheck for " + age);
#endif
					}

					////////////////////////////////////////////////////////////////////
					// Native callbacks
					////////////////////////////////////////////////////////////////////

					public void nativeCallback(string payload) {
						// try to get payload and type data
						try {
							GetIsMinorModel model = JsonUtility.FromJson<GetIsMinorModel>(payload);
							callback(model);
						} catch {
							Debug.Log ("Error parsing GetIsMinorModel");
							return;
						}
					}
				}
			}
		}
	}
}

