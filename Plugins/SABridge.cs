using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SABridge{

#if UNITY_ANDROID  && !UNITY_EDITOR
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

	public static void openVideoAd(string placementId){
		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		AndroidJavaClass ajc2 = new AndroidJavaClass ("tv.superawesome.mobile.view.VideoAdActivity");
		ajc2.CallStatic ("openInActivity", jo);
	}
	public static bool openParentalGate(string url){
		//Return false because not implemented; the application will take care of the event
		return false;
	}

#elif UNITY_IPHONE && !UNITY_EDITOR
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAd(int appId, string placementId);

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
	
	public static void openVideoAd(string placementId){
		SABridge.SuperAwesomeUnityOpenVideoAd (SABridge.appId, placementId);
	}
	
	public static bool openParentalGate(string url){
		SABridge.SuperAwesomeUnityOpenParentalGate(url);
		return true;
	}
#else
	public static string getVersion(){
		return "Unsupported Platform";
	}
	
	public static void setAppId(int appId){
	}
	
	public static void openVideoAd(string placementId){
	}

	public static bool openParentalGate(string url){
		//Return false because not implemented; the application will take care of the event
		return false;
	}
#endif

}
