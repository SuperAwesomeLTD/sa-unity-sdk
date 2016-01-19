using UnityEngine;
using System;

namespace SuperAwesome{ 

	/**
 	 * This is a simplified model SAAd - the rest of the heavy lifiting will
 	 * be done by the iOS / Android SDKs
 	 */
	public class SAAd {

		/** the ad Json */
		public string adJson = null;

		/** the ID of the placement that the ad was sent for */
		public int placementId = -1;
		
		public SAAd() {
			/** do nothing */
		}
	}
}