using System;
using UnityEngine;
using System.Collections;

public class SuperAwesome : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	public static string getVersion(){
		return "SuperAwesome Unity SDK version 1.1 ("+SABridge.getVersion()+")";
	}

	public static void setAppId(int appId){
		SABridge.setAppId (appId);
	}

	public static void openVideoAd(string placementId){
		SABridge.openVideoAd(placementId);
	}


	public static void openParentalGate(string url){
		SABridge.openParentalGate(url);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
