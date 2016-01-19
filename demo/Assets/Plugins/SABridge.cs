using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SABridge {

#if UNITY_ANDROID  && !UNITY_EDITOR

	public static void loadAd(
		string unityName, 
		int placementId, 
		bool isTestingEnabled) 
	{
		// do nothing
	}

	public static void openVideoAd(
		string unityName, 
		int placementId, 
		string adJson, 
		bool isParentalGateEnabled, 
		bool shouldShowCloseButton,
		bool shouldAutomaticallyCloseAtEnd)
	{
		var androidJC = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		var jo = androidJC.GetStatic<AndroidJavaObject> ("currentActivity");

		var activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
			AndroidJavaClass test = new AndroidJavaClass("tv.superawesome.plugins.unity.SAVideoActivityUnityLinker");
			test.CallStatic("start", jo, unityName, placementId, false, isParentalGateEnabled);
		}));
	}

#elif UNITY_IPHONE && !UNITY_EDITOR

	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityLoadAd(string unityName, int placementId, bool isTestingEnabled);
	[DllImport ("__Internal")]
	private static extern void SuperAwesomeUnityOpenVideoAd(string unityName, int placementId, string adJson, bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldAutomaticallyCloseAtEnd);
	
	public static void loadAd(
		string unityName, 
		int placementId, 
		bool isTestingEnabled) 
	{
		SABridge.SuperAwesomeUnityLoadAd(unityName, placementId, isTestingEnabled);
	}

	public static void openVideoAd(
		string unityName, 
		int placementId, 
		string adJson, 
		bool isParentalGateEnabled, 
		bool shouldShowCloseButton,
		bool shouldAutomaticallyCloseAtEnd)
	{
		SABridge.SuperAwesomeUnityOpenVideoAd (unityName, placementId, adJson, isParentalGateEnabled, shouldShowCloseButton, shouldAutomaticallyCloseAtEnd);
	}

#else

	public static void loadAd(
		string unityName, 
		int placementId, 
		bool isTestingEnabled) 
	{
		// do nothing
	}

	public static void openVideoAd(
		string unityName, 
		int placementId, 
		string adJson, 
		bool isParentalGateEnabled, 
		bool shouldShowCloseButton,
		bool shouldAutomaticallyCloseAtEnd)
	{
		// do nothing
	}

#endif

}