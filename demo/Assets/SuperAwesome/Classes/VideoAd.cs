// imports
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.EventSystems;

// part of the SuperAwesome namespace
namespace SuperAwesome {

	// class implementation and definition
	public class VideoAd : MonoBehaviour {

		////////////////////////////////////////////////////
		/// Member variables
		////////////////////////////////////////////////////

		// 1. video and ad specific variables
		public String placementID = "";
		public bool testMode = true;
		public bool isParentalGateEnabled = false;
		public bool shouldAutoStart = true;

		// 2. delegate functions
		public delegate void VideoWasLoadedHandler();
		public event VideoWasLoadedHandler OnVideoAdWasLoaded;
		public delegate void VideoFailedToLoadHandler();
		public event VideoFailedToLoadHandler OnVideoAdFailedToLoad;
		public delegate void VideoStartedPlayingHandler();
		public event VideoStartedPlayingHandler OnVideoStartedPlaying;
		public delegate void VideoStoppedPlayingHandler();
		public event VideoStoppedPlayingHandler OnVideoStoppedPlaying;
		public delegate void VideoFailedToPlayHandler();
		public event VideoFailedToPlayHandler OnVideoFailedToPlay;
		public delegate void VideoClickedHandler();
		public event VideoClickedHandler OnVideoClicked;

		////////////////////////////////////////////////////
		/// Initialise Video Ad
		////////////////////////////////////////////////////

		void Start (){

			// just call show
			if (this.shouldAutoStart == true) {
				Show ();
			} else {
				Debug.Log("Started video ad, but not yet showing");
			}
		}
		
		void Update () {
			// do nothing here
		}

		////////////////////////////////////////////////////
		/// Load the Interstitial ad
		////////////////////////////////////////////////////

		private void Show() {

			// then call to open - this function only exists to
			// miminc Banner and Interstitial View code
			this.open ();
		}

		public void open() {

#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
			SABridge.openVideoAd(this.name, this.placementID, this.isParentalGateEnabled, this.testMode);
#else
			Debug.Log ("Tried to start video");
#endif
		}

		////////////////////////////////////////////////////
		/// Wrappers used in message passing from the ObjC 
		/// plugin to Unity
		////////////////////////////////////////////////////

		public void videoAdLoaded() {
			Debug.Log ("videoAdLoaded = Was called from Android");

			if (OnVideoAdWasLoaded != null)
				OnVideoAdWasLoaded ();
		}

		public void videoAdFailedToLoad() {
			Debug.Log ("videoAdFailedToLoad - Was called from Android");

			if (OnVideoAdFailedToLoad != null)
				OnVideoAdFailedToLoad ();
		}

		public void videoAdStartedPlaying() {
			Debug.Log ("videoAdStartedPlaying- Was called from Android");

			if (OnVideoStartedPlaying != null) 
				OnVideoStartedPlaying ();
		}

		public void videoAdStoppedPlaying() {
			Debug.Log ("videoAdStoppedPlaying- Was called from Android");

			if (OnVideoStoppedPlaying != null) 
				OnVideoStoppedPlaying ();
		}

		public void videoAdFailedToPlay() {
			Debug.Log ("videoAdFailedToPlay - Was called from Android");

			if (OnVideoFailedToPlay != null) 
				OnVideoFailedToPlay ();
		}

		public void videoAdClicked() {
			Debug.Log ("videoAdClicked - Was called from Android");

			if (OnVideoClicked != null)
				OnVideoClicked ();
		}
	}
}