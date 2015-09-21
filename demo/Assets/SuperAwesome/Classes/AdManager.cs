using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class AdManager
	{
		public String baseUrl { get; set; }

		public AdManager (String baseUrl)
		{
			this.baseUrl = baseUrl;
		}

		public IEnumerator getAd(String placementID, bool testMode, Action<Ad> Callback)
		{
			string url = this.baseUrl + "/ad/" + placementID;
			url = testMode ? url + "?test=true" : url;

			WWW adData = new WWW(url);
			yield return adData;

			if (adData.error != null)
			{
				Debug.Log (adData.error);
				Callback (null);
				yield return null;
			} else {
				try {
					Debug.Log (adData.text);
					Ad ad = new Ad (adData.text);
					ad.placementId = placementID;
					Callback (ad);
				} catch {
					Callback (null);
				}
			}
		}
	}
}

