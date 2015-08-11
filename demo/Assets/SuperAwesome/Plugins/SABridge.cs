using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SABridge {

#if UNITY_ANDROID  && !UNITY_EDITOR
/*
	public static string getVersion(){
		AndroidJavaClass ajc = new AndroidJavaClass ("tv.superawesome.mobile.SuperAwesome");
		string version = ajc.GetStatic<string> ("VERSION");
		return "SuperAwesome Android SDK version " + version;
	}

	public static void setAppId(int appId){
		AndroidJavaClass ajc = new AndroidJavaClass ("tv.superawesome.mobile.SuperAwesome");
		AndroidJavaObject sa = ajc.CallStatic<AndroidJavaObject> ("getInstance");
		sa.Call ("setAppId", appId);
	}
*/
	public static void openVideoAd(string placementId, bool testMode){
		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		AndroidJavaClass videoActivity = new AndroidJavaClass ("tv.superawesome.superawesomesdk.activities.SAVideoActivity");
		videoActivity.CallStatic ("start", jo, "5740");
	}
/*

	public static bool openParentalGate(string url){

		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
				AndroidJavaObject parentalGate = new AndroidJavaObject("tv.superawesome.mobile.ParentalGate", jo);

				ParentalGateViewCallback cb = new ParentalGateViewCallback();
				cb.setUrl(url);
				parentalGate.Call("setViewCallback", cb);
			})
		);
		return true;
	}
*/

#elif UNITY_IPHONE && !UNITY_EDITOR

	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAd(string placementId);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAdTestmode (string placementId);
/*
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenParentalGate(string url);

	private static int appId = 14;

	public static string getVersion(){
		//TODO get version from SDK
		return "iOS";
	}
	
	public static void setAppId(int appId){
		SABridge.appId = appId;
	}
*/	
	public static void openVideoAd(string placementId, bool testMode){
		if (testMode) {
			SABridge.SuperAwesomeUnityOpenVideoAdTestmode (placementId);
		} else {
			SABridge.SuperAwesomeUnityOpenVideoAd (placementId);
		}
	}
/*	
	public static bool openParentalGate(string url){
		SABridge.SuperAwesomeUnityOpenParentalGate(url);
		return true;
	}
*/
#else
/*
	public static string getVersion(){
		return "Unsupported Platform";
	}
	
	public static void setAppId(int appId){
	}
*/
	public static void openVideoAd(string placementId, bool testMode){
	}
/*
	public static bool openParentalGate(string url){
		//Return false because not implemented; the application will take care of the event
		return false;
	}
*/
#endif

}
