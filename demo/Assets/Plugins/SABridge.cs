using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SABridge {

#if UNITY_ANDROID  && !UNITY_EDITOR

	public static void openVideoAd(string placementId, bool testMode){
		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		AndroidJavaClass videoActivity = new AndroidJavaClass ("tv.superawesome.superawesomesdk.activities.SAVideoActivity");
		videoActivity.CallStatic ("start", jo, "5740");
	}

	public static void showParentalGate(string adName) {
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
	private static extern void SuperAwesomeUnityOpenVideoAd(string placementId);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAdTestmode (string placementId);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenParentalGate(string adName);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityShowPadlockView();

	public static void openVideoAd(string placementId, bool testMode){
		if (testMode) {
			SABridge.SuperAwesomeUnityOpenVideoAdTestmode (placementId);
		} else {
			SABridge.SuperAwesomeUnityOpenVideoAd (placementId);
		}
	}

	public static void showParentalGate(string adName) {
		SABridge.SuperAwesomeUnityOpenParentalGate(adName);
	}

	public static void showPadlockView(){
		SABridge.SuperAwesomeUnityShowPadlockView();
	}

#else

	public static void openVideoAd(string placementId, bool testMode){
		// do nothing
	}

	public static void showParentalGate(string adName) {
		// do nothing
	}

	public static void showPadlockView(){
		// do nothing
	}

#endif

}