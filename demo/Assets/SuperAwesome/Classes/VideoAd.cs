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
			SABridge.openVideoAd(this.placementID, this.isParentalGateEnabled, this.testMode);
#else
			Debug.Log ("Tried to start video");
#endif
		}
	}
}