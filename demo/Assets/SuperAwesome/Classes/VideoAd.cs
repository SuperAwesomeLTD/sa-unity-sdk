using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.EventSystems;

namespace SuperAwesome
{
	public class VideoAd : MonoBehaviour {

		public String placementID = "";
		public bool testMode = true;
		public bool isParentalGateEnabled = false;
		public bool shouldAutoStart = true;

		// Use this for initialization
		void Start (){
			if (this.shouldAutoStart == true) {
				this.open ();
			}
		}
		
		// Update is called once per frame
		void Update () {
			// do nothing here
		}
		
		private void Show() {

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