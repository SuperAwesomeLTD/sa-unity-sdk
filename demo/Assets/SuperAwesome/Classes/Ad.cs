using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

namespace SuperAwesome
{
	public class Ad
	{
		public string imageURL { get; private set; }
		public string clickURL { get; private set; }
		public Int64 width { get; private set; }
		public Int64 height { get; private set; }
		public Texture2D texture { get; private set; }
		public bool fallback { get; private set; }

		// other members that should be in a struct of their own
		public String placementId { get; set; }
		public Int64 lineItemId { get; set; }
		public Int64 creativeId { get; set; }

		public Ad(string jsonString)
		{
			Debug.Log (jsonString);
			try {
				Dictionary<string, object> ad = Json.Deserialize(jsonString) as Dictionary<string, object>;
				Dictionary<string, object> creative = ad ["creative"] as Dictionary<string, object>;
				Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;

				this.fallback = (bool) ad["is_fallback"];
				this.imageURL = (string) details["image"];
				this.width = (Int64) details["width"];
				this.height = (Int64) details["height"];
				this.clickURL = (string) creative["click_url"];

				this.lineItemId = (Int64) ad["line_item_id"];
				this.creativeId = (Int64) creative["id"];
 			} catch {
				throw new ArgumentException("JSON argument not valid");
			}
		}

		public IEnumerator LoadImage(Action Callback) {
			WWW image = new WWW(this.imageURL);
			yield return image;
			
			this.texture = image.texture;
			Callback ();
		}
	}
}