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


		public Ad(string jsonString)
		{
			try {
				Dictionary<string, object> ad = Json.Deserialize(jsonString) as Dictionary<string, object>;
				Dictionary<string, object> creative = ad ["creative"] as Dictionary<string, object>;
				Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;

				this.imageURL = (string) details["image"];
				this.width = (Int64) details["width"];
				this.height = (Int64) details["height"];
				this.clickURL = (string) creative["click_url"];
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