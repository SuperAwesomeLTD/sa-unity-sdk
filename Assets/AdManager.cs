using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class AdManager
	{
		public String baseUrl { get; set; }

		public delegate IEnumerator Load(Ad ad);


		public AdManager (String baseUrl)
		{
			this.baseUrl = baseUrl;
		}

		public IEnumerator getAd(String placementID, Load Callback)
		{
			
			Debug.Log ("123");
			string url = "http://staging.beta.ads.superawesome.tv/v2/ad/38";// + placementID;
			WWW ad_data = new WWW(url);
			yield return ad_data;

			if (ad_data.error != null) {
				Callback (null);
				yield return null;
			}

			try {
				Ad ad = new Ad (ad_data.text);
				Debug.Log (Callback);
				Debug.Log (Callback.GetType());
				Callback(ad);
				Debug.Log ("Test");
			} catch {
				Callback (null);
			}


//			String response = "{ \"line_item_id\":1, \"campaign_id\":1, \"creative\":{ \"id\":1, \"format\":\"image_with_link\", \"click_url\": \"http://superawesome.tv\", \"details\": { \"image\":\"http://www.helpinghomelesscats.com/images/cat1.jpg\", \"width\":728, \"height\":90 } } }";
//			String response = ad_data.text;
//
//			Dictionary<string, object> ad = Json.Deserialize(response) as Dictionary<string, object>;
//			if (ad == null) {
//				Debug.Log ("ad is null");
//			}
//			Callback ();
		}
	}
}

