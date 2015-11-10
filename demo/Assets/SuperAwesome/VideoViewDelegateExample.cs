using System;
using UnityEngine;
using System.Collections;
using SuperAwesome;

public class VideoViewDelegateExample : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		VideoAd video = this.GetComponentInChildren<VideoAd> ();
		video.OnVideoAdWasLoaded += didLoadVideoAd;
		video.OnVideoAdFailedToLoad += didFailToLoadVideoAd;
		video.OnVideoStartedPlaying += didStartVideoAd;
		video.OnVideoStoppedPlaying += didStopVideoAd;
		video.OnVideoFailedToPlay += didFailToPlayVideoAd;
		video.OnVideoClicked += didClickOnVideoAd;
		
		GetComponent<Canvas> ().sortingOrder = 1000;
	}

	void didLoadVideoAd(){
		Debug.Log ("Video Delegate - ad loaded");
	}

	void didFailToLoadVideoAd() {
		Debug.Log ("Video Delegate - ad failed to load");
	}

	void didStartVideoAd() {
		Debug.Log ("Video Delegate - start");
	}

	void didStopVideoAd() {
		Debug.Log ("Video Delegate - stop");
	}

	void didFailToPlayVideoAd() {
		Debug.Log ("Video Delegate - fail to play");
	}

	void didClickOnVideoAd() {
		Debug.Log ("Video Delegate - click");
	}
}
