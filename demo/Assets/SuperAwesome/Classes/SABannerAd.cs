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

				public class SABannerAd: MonoBehaviour {

#if (UNITY_IPHONE && !UNITY_EDITOR)

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySABannerAdCreate(string unityName);

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySABannerAdLoad(string unityName,
					int placementId,
					int configuration, 
					bool test);

					[DllImport ("__Internal")]
					private static extern bool SuperAwesomeUnitySABannerAdHasAdAvailable(string unityName);


					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySABannerAdPlay(string unityName,
					bool isParentalGateEnabled, 
					bool isBumperPageEnabled,
					int position, 
					int width,
					int height,
					bool color);

					[DllImport ("__Internal")]
					private static extern void SuperAwesomeUnitySABannerAdClose(string unityName);

#endif

					// banner index
					private static uint 					index = 0;

					// define a default callback so that it's never null and I don't have
					// to do a check every time I want to call it
					private Action <int, SAEvent>	    	callback = (p, e) => {};

					// private state vars
					private bool isParentalGateEnabled 		= SADefines.defaultParentalGate ();
					private bool isBumperPageEnabled		= SADefines.defaultBumperPage ();
					private SABannerPosition position 		= SADefines.defaultBannerPosition ();
					private int bannerWidth 				= SADefines.defaultBannerWidth ();
					private int bannerHeight 				= SADefines.defaultBannerHeight ();
					private bool color 						= SADefines.defaultBgColor ();
					private SAConfiguration configuration 	= SADefines.defaultConfiguration ();
					private bool isTestingEnabled 			= SADefines.defaultTestMode ();

					// create method
					public static SABannerAd createInstance() {

						GameObject obj = new GameObject ();
						SABannerAd adObj = obj.AddComponent<SABannerAd> ();
						adObj.name = "SABannerAd_" + (++SABannerAd.index);
						DontDestroyOnLoad (obj);

						//
						// set native version
						SAVersion.setVersionInNative ();

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SABannerAd.SuperAwesomeUnitySABannerAdCreate(adObj.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var nameL = adObj.name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBannerAd");
							saplugin.CallStatic("SuperAwesomeUnitySABannerAdCreate", context, nameL);
						}));

#else 
						Debug.Log (adObj.name + " Create");
#endif

						return adObj;
					}

					////////////////////////////////////////////////////////////////////
					// Banner specific method
					////////////////////////////////////////////////////////////////////

					public void load (int placementId) {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SABannerAd.SuperAwesomeUnitySABannerAdLoad(this.name, placementId, (int)configuration, isTestingEnabled);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var nameL = this.name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBannerAd");
							saplugin.CallStatic("SuperAwesomeUnitySABannerAdLoad", context, nameL, placementId, (int)configuration, isTestingEnabled);
						}));

#else
						Debug.Log (this.name + " Load");
#endif

					}

					public void play () {
					
#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SABannerAd.SuperAwesomeUnitySABannerAdPlay(this.name, isParentalGateEnabled, isBumperPageEnabled, (int)position, bannerWidth, bannerHeight, color);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
						var nameL = this.name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBannerAd");
						saplugin.CallStatic("SuperAwesomeUnitySABannerAdPlay", context, nameL, isParentalGateEnabled, isBumperPageEnabled, (int)position, bannerWidth, bannerHeight, color);
						}));

#else 
						Debug.Log (this.name + " Play");
#endif

					}

					public bool hasAdAvailable () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						return SABannerAd.SuperAwesomeUnitySABannerAdHasAdAvailable(this.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)
						var nameL = this.name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");
						var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBannerAd");

						return saplugin.CallStatic<bool> ("SuperAwesomeUnitySABannerAdHasAdAvailable", context, nameL);
#else 
						Debug.Log (this.name + " HasAdAvailable");
#endif
						return false;

					}

					public void close () {

#if (UNITY_IPHONE && !UNITY_EDITOR) 
						SABannerAd.SuperAwesomeUnitySABannerAdClose(this.name);
#elif (UNITY_ANDROID && !UNITY_EDITOR)

						var nameL = this.name;

						var unityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						var context = unityClass.GetStatic<AndroidJavaObject> ("currentActivity");

						context.Call("runOnUiThread", new AndroidJavaRunnable(() => {
							var saplugin = new AndroidJavaClass ("tv.superawesome.plugins.publisher.unity.SAUnityBannerAd");
							saplugin.CallStatic("SuperAwesomeUnitySABannerAdClose", context, nameL);
						}));

#else 
						Debug.Log (this.name + " Close");
#endif 
					}

					////////////////////////////////////////////////////////////////////
					// Setters & getters
					////////////////////////////////////////////////////////////////////

					public void setCallback (Action<int, SAEvent> value) {
						this.callback = value != null ? value : this.callback;
					}

					public void enableParentalGate () {
						isParentalGateEnabled = true;
					}

					public void disableParentalGate () {
						isParentalGateEnabled = false;
					}

					public void enableBumperPage () {
						isBumperPageEnabled = true; 
					}

					public void disableBumperPage () {
						isBumperPageEnabled = false;
					}

					public void enableTestMode () {
						isTestingEnabled = true;
					}

					public void disableTestMode () {
						isTestingEnabled = false;
					}

					public void setConfigurationProduction () {
						configuration = SAConfiguration.PRODUCTION;
					}

					public void setConfigurationStaging () {
						configuration = SAConfiguration.STAGING;
					}

					public void setPositionTop () {
						position = SABannerPosition.TOP;
					}

					public void setPositionBottom () {
						position = SABannerPosition.BOTTOM;
					}

					public void setSize_300_50 () {
						bannerWidth = 300;
						bannerHeight = 50;
					}

					public void setSize_320_50 () {
						bannerWidth = 320;
						bannerHeight = 50;
					}

					public void setSize_728_90 () {
						bannerWidth = 728;
						bannerHeight = 90;
					}

					public void setSize_300_250 () {
						bannerWidth = 300;
						bannerHeight = 250;
					}

					public void setColorGray () {
						color = false;
					}

					public void setColorTransparent () {
						color = true;
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
