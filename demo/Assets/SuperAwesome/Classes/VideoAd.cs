using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.EventSystems;

namespace SuperAwesome
{
	public class VideoAd {

		public static void open(string placementID, bool testMode) {
			SABridge.openVideoAd(placementID, testMode);
		}
		
		public static void open(string placementID) {
			open (placementID, false);
		}
	}
}