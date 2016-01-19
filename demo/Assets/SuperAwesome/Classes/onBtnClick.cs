using UnityEngine;
using System;
using System.Collections;

namespace SuperAwesome {

	public class onBtnClick : MonoBehaviour {

		private SAAd ad = null;

		// Use this for initialization
		void Start () {
			// do nothing
		}
		
		// Update is called once per frame
		void Update () {
			// do nothing
		}
		
		// button actions
		public void loadVideoAction () {
			SALoader.createInstance().loadAd (28000, (Ad) => {
				this.ad = Ad;
				Debug.Log("Loaded " + ad.placementId);
				Debug.Log("And " + ad.adJson);
				return 0;
			}, (placementId) => {
				Debug.Log("Failed to preload: " + placementId.ToString());
				return 0;
			});
		}

		public void playVideoAction () {
			if (ad != null) {
				SAVideoAd vad = SAVideoAd.createInstance ();
				vad.setAd(ad);
				vad.isParentalGateEnabled = true;
				vad.shouldShowCloseButton = false;
				vad.shouldAutomaticallyCloseAtEnd = true;
				vad.play();
			} else {
				Debug.Log("Ad is still not loaded!");
			}
		}
		
		public void playVideoDirectlyAction () {
			SAVideoAd.showAd (28000, false, true, false);
		}
	}

}

