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

		public delegate void BannerWasLoadedHandler();
		public event BannerWasClickedHandler OnBannerWasLoaded;
		public delegate void BannerWasClickedHandler();
		public event BannerWasClickedHandler OnBannerWasClicked;

		private Button button;
		private Image image;
		private Dictionary<string, object> ad;
		
		private Int64 _width;
		private Int64 _height;

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
			float x;
			float y;
			switch (this.layout) {
				case Layout.Bottom:
					x = Screen.width / 2;
					y = this._height / 2;
					break;
				case Layout.Top:
					x = Screen.width / 2;
					y = Screen.height - this._height/2;
					break;
				default:
					break;
			}
			transform.position = new Vector3 (x, y, transform.position.z);
		}

		private IEnumerator loadAd(){
			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;
			string imgurl = (string) details["image"];
			this._width = (Int64) details["width"];
			this._height = (Int64) details["height"];
			Debug.Log (imgurl);
			
			WWW image = new WWW(imgurl);
			yield return image;
			Debug.Log (details ["width"].GetType());
			Texture2D texture = image.texture;

			//Resize button using its RectTransform component
			this.button.image.rectTransform.sizeDelta = new Vector2 (this._width, this._height);

			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;

			show ();

			if(OnBannerWasLoaded != null) OnBannerWasLoaded();
		}

		private void OnClick(){
			Debug.Log ("KLIKK");

			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			String clickURL = creative ["click_url"] as String;
			Debug.Log (clickURL);
			Application.OpenURL(clickURL);

			if(OnBannerWasClicked != null) OnBannerWasClicked();
		}	

	}
}