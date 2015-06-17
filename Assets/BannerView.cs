using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class BannerView : MonoBehaviour {

		public String placementID = "Your Placement ID";
		public enum Layout { Manual, Top, Bottom };
		public Layout layout = Layout.Manual;

		private Button button;
		private Image image;
		private Dictionary<string, object> ad;

		// Use this for initialization
		void Start () {
			StartCoroutine(loadAd());

			this.image = this.GetComponent<Image>();

			this.button = this.GetComponent<Button>();
			this.button.onClick.AddListener (() => OnClick ());

			align ();
			hide ();
		}

		// Update is called once per frame
		void Update () {

		}

		private void show(){
			this.image.color = Color.white;
		}

		private void hide(){
			this.image.color = Color.clear;
		}

		private void align(){
			if (this.layout == Layout.Bottom) {
				float x = Screen.width / 2;
				float y = 50 / 2;
				transform.position = new Vector3 (x, y, transform.position.z);
			} else if (this.layout == Layout.Top) {
				float x = Screen.width / 2;
				float y = Screen.height - 50/2;
				transform.position = new Vector3 (x, y, transform.position.z);
			}
		}

		private IEnumerator loadAd(){
			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;
			string imgurl = (string) details["image"];
			Debug.Log (imgurl);
			
			WWW image = new WWW(imgurl);
			yield return image;
			
			Texture2D texture = image.texture;
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;

			show ();
		}

		private void OnClick(){
			Debug.Log ("KLIKK");

			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			String clickURL = creative ["click_url"] as String;
			Debug.Log (clickURL);
			Application.OpenURL(clickURL);
		}	

	}
}