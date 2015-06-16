using UnityEngine;
using System;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome
{
	public class AdManager
	{
		public String baseUrl { get; set; }

		public AdManager (String baseUrl)
		{
			this.baseUrl = baseUrl;
		}

		public Dictionary<string, object> getAd(String placementID)
		{
			String response = "{ \"line_item_id\":1, \"campaign_id\":1, \"creative\":{ \"id\":1, \"format\":\"image_with_link\", \"click_url\": \"http://superawesome.tv\", \"details\": { \"image\":\"http://www.helpinghomelesscats.com/images/cat1.jpg\", \"width\":728, \"height\":90 } } }";
			Dictionary<string, object> ad = Json.Deserialize(response) as Dictionary<string, object>;
			if (ad == null) {
				Debug.Log ("ad is null");
			}
			return ad;
		}
	}
}

