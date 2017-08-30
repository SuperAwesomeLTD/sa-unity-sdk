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

				public class SAAppWall : MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)
					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySAAppWallCreate ();


					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySAAppWallLoad(int placementId, int configuration,  bool test);

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySAAppWallPlay(int placementId, bool isParentalGateEnabled, bool isBumperPageEnabled);

					[DllImport ("__Internal")]
					private static extern bool SuperAwesomeUnitySAAppWallHasAdAvailable(int placementId);
#endif

					// the app wall ad static instance
					private static SAAppWall		 			staticInstance = null;

					// define a default callback so that it's never null and I don't have
					// to do a check every time I want to call it
					private static Action<int, SAEvent>			callback = (p, e) => {};

					// assign default values to all of these fields
					private static bool isParentalGateEnabled 		= SADefines.defaultParentalGate ();
					private static bool isBumperPageEnabled 		= SADefines.defaultBumperPage ();
					private static bool isTestingEnabled 			= SADefines.defaultTestMode ();
					private static bool	isBackButtonEnabled 		= SADefines.defaultBackButton ();
					private static SAConfiguration configuration 	= SADefines.defaultConfiguration ();

					// instance constructor
					private static void createInstance () {

						// create just one static instance for ever!
						if (staticInstance == null) {
							GameObject obj = new GameObject ();
							staticInstance = obj.AddComponent<SAAppWall> ();
							staticInstance.name = "SAAppWall";
							DontDestroyOnLoad (staticInstance);

							//
							// set native version
							SAVersion.setVersionInNative ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
							SAAppWall.SuperAwesomeUnitySAAppWallCreate ();
#elif (UNITY_ANDROID && !UNITY_EDITOR)

							var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
							var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

							context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
								var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAppWall");
								saplugin.CallStatic("SuperAwesomeUnitySAAppWallCreate", context);
							}));

#else 
							Debug.Log("SAAppWall Create");
#endif
						}
					}

					////////////////////////////////////////////////////////////////////
					// GameWall specific methods
					////////////////////////////////////////////////////////////////////

					public static void load (int placementId) {

						// create an instrance of an SAAppWall (for callbacks)
						createInstance ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SAAppWall.SuperAwesomeUnitySAAppWallLoad(placementId, (int)configuration, isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAppWall");
							saplugin.CallStatic("SuperAwesomeUnitySAAppWallLoad", 
								context, 
								placementId, 
								(int)configuration, 
								isTestingEnabled);
							}));

#else
						Debug.Log ("SAAppWall Load");
#endif

					}

					public static void play (int placementId) {

						// create an instrance of an SAAppWall (for callbacks)
						createInstance ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SAAppWall.SuperAwesomeUnitySAAppWallPlay(placementId, isParentalGateEnabled, isBumperPageEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAppWall");
							saplugin.CallStatic("SuperAwesomeUnitySAAppWallPlay", 
								context, 
								placementId, 
								isParentalGateEnabled,
								isBumperPageEnabled,
								isBackButtonEnabled);
							}));

#else
						Debug.Log ("SAAppWall Play");
#endif
					}

					public static bool hasAdAvailable (int placementId) {

						// create an instrance of an SAAppWall (for callbacks)
						createInstance ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						return SAAppWall.SuperAwesomeUnitySAAppWallHasAdAvailable(placementId);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityAppWall");

						return saplugin.CallStatic<bool>("SuperAwesomeUnitySAAppWallHasAdAvailable", context, placementId);

#else 
						Debug.Log ("SAAppWall HasAdAvailable");
						return false;
#endif
						return false;
					}

					////////////////////////////////////////////////////////////////////
					// Setters & getters
					////////////////////////////////////////////////////////////////////

					public static void setCallback (Action<int, SAEvent> value) {
						callback = value != null ? value : callback;
					}

					public static void enableParentalGate () {
						isParentalGateEnabled = true;
					}

					public static void disableParentalGate () {
						isParentalGateEnabled = false;
					}

					public static void enableBumperPage () {
						isBumperPageEnabled = true; 
					}

					public static void disableBumperPage () {
						isBumperPageEnabled = false;
					}

					public static void enableTestMode () {
						isTestingEnabled = true;
					}

					public static void disableTestMode () {
						isTestingEnabled = false;
					}

					public static void setConfigurationProduction () {
						configuration = SAConfiguration.PRODUCTION;
					}

					public static void setConfigurationStaging () {
						configuration = SAConfiguration.STAGING;
					}

					public static void enableBackButton () {
						isBackButtonEnabled = true;
					}

					public static void disableBackButton () {
						isBackButtonEnabled = false;
					}

					////////////////////////////////////////////////////////////////////
					// Native callbacks
					////////////////////////////////////////////////////////////////////

					public void nativeCallback(string payload) {
						Dictionary<string, object> payloadDict;
						string type = "";
						int placementId = 0;

						// try to get payload and type data
						try {
							payloadDict = Json.Deserialize (payload) as Dictionary<string, object>;
							type = (string) payloadDict["type"];
							string plac = (string) payloadDict["placementId"];
							int.TryParse(plac, out placementId);
						} catch {
							Debug.Log ("Error w/ callback!");
							return;
						}

						switch (type) {
						case "sacallback_adLoaded":  callback(placementId, SAEvent.adLoaded); break;
						case "sacallback_adEmpty": callback (placementId, SAEvent.adEmpty); break;
						case "sacallback_adFailedToLoad": callback(placementId, SAEvent.adFailedToLoad); break;
						case "sacallback_adAlreadyLoaded": callback (placementId, SAEvent.adAlreadyLoaded); break;
						case "sacallback_adShown": callback(placementId, SAEvent.adShown); break;
						case "sacallback_adFailedToShow": callback (placementId, SAEvent.adFailedToShow); break;
						case "sacallback_adClicked": callback (placementId, SAEvent.adClicked); break;
						case "sacallback_adEnded": callback (placementId, SAEvent.adEnded); break;
						case "sacallback_adClosed": callback (placementId, SAEvent.adClosed); break;
						}
					}
				}
			}
		}
	}
}

