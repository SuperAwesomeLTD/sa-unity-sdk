using System;
using UnityEngine;
using System.Collections;

public class SuperAwesome : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	public static string getVersion(){
		return "SuperAwesome Unity SDK version 1.0 ("+SABridge.getVersion()+")";
	}

	public static void setAppId(int appId){
		SABridge.setAppId (appId);
	}

	public static void openVideoAd(){
		SABridge.openVideoAd();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
