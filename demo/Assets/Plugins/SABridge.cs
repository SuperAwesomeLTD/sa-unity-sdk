using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SABridge {

#if UNITY_ANDROID  && !UNITY_EDITOR

	public static void openVideoAd(string adName, string placementId, bool gateEnabled, bool testMode){
		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAVideoActivityUnityLinker");
			test.CallStatic("start", jo, adName, placementId, testMode, gateEnabled);
		}));
	}

	public static void showParentalGate(string adName, string placementId, long creativeId, long lineItemId) {
		var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			AndroidJavaObject parentalGate = new AndroidJavaObject("tv.superawesome.sdk.parentalgate.SAParentalGateStandalone");
			parentalGate.Call("createParentalGate", jo, adName);
		}));
	}

	public static void showPadlockView(){
		// do nothing
		var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			AndroidJavaObject parentalGate = new AndroidJavaObject("tv.superawesome.sdk.padlock.SAPadlock");
			parentalGate.Call("createPadlock", jo);
		}));
	}


#elif UNITY_IPHONE && !UNITY_EDITOR

	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAd(string adName, string placementId, bool gateEnabled, bool testMode);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenParentalGate(string adName, string placementId, long creativeId, long lineItemId);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityShowPadlockView();

	public static void openVideoAd(string adName, string placementId, bool gateEnabled, bool testMode){
		SABridge.SuperAwesomeUnityOpenVideoAd (adName, placementId, gateEnabled, testMode);
	}

	public static void showParentalGate(string adName, string placementId, long creativeId, long lineItemId) {
		SABridge.SuperAwesomeUnityOpenParentalGate(adName, placementId, creativeId, lineItemId);
	}

	public static void showPadlockView(){
		SABridge.SuperAwesomeUnityShowPadlockView();
	}


#else

	public static void openVideoAd(string adName, string placementId, bool gateEnabled, bool testMode){
		// do nothing
	}

	public static void showParentalGate(string adName, string placementId, long creativeId, long lineItemId) {
		// do nothing
	}

	public static void showPadlockView(){
		// do nothing
	}


#endif

}