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

		public IEnumerator getAd(String placementID, Action<Ad> Callback)
		{
			string url = "http://staging.beta.ads.superawesome.tv/v2/ad/" + placementID;
			WWW ad_data = new WWW(url);
			yield return ad_data;

			if (ad_data.error != null)
			{
				Debug.Log (ad_data.error);
				Callback (null);
				yield return null;
			} else {
				Debug.Log (ad_data.text);
				try {
					Ad ad = new Ad (ad_data.text);
					Callback (ad);
				} catch {
					Callback (null);
				}
			}
		}
	}
}

