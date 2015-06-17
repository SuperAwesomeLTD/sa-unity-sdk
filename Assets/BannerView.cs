using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class BannerView : MonoBehaviour {

		public enum Layout { Manual, Top, Bottom };

		public String placementID = "Your Placement ID";
		public Layout layout = Layout.Manual;

		public delegate void BannerWasLoadedHandler();
		public event BannerWasClickedHandler OnBannerWasLoaded;
		public delegate void BannerWasClickedHandler();
		public event BannerWasClickedHandler OnBannerWasClicked;

		private Button button;
		private Image image;
		private Dictionary<string, object> ad;
		private Int64 width;
		private Int64 height;

		// Use this for initialization
		void Start ()
		{
			StartCoroutine(Load());

			this.image = this.GetComponent<Image>();

			this.button = this.GetComponent<Button>();
			this.button.onClick.AddListener (() => OnClick ());

			Align ();
			Hide ();
		}

		// Update is called once per frame
		void Update ()
		{

		}

		private void Show()
		{
			this.image.color = Color.white;
		}

		private void Hide()
		{
			this.image.color = Color.clear;
		}

		private void Align()
		{
			float x;
			float y;
			switch (this.layout)
			{
				case Layout.Bottom:
					x = Screen.width / 2;
					y = this.height / 2;
					break;
				case Layout.Top:
					x = Screen.width / 2;
					y = Screen.height - this.height/2;
					break;
				default:
					x = transform.position.x;
					y = transform.position.y;
					break;
			}
			transform.position = new Vector3 (x, y, transform.position.z);
		}

		private IEnumerator Load()
		{
			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;
			string imgurl = (string) details["image"];
			this.width = (Int64) details["width"];
			this.height = (Int64) details["height"];
			Debug.Log (imgurl);
			
			WWW image = new WWW(imgurl);
			yield return image;
			Debug.Log (details ["width"].GetType());
			Texture2D texture = image.texture;

			//Resize button using its RectTransform component
			this.button.image.rectTransform.sizeDelta = new Vector2 (this.width, this.height);

			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;

			Show ();

			if(OnBannerWasLoaded != null) OnBannerWasLoaded();
		}

		private void OnClick()
		{
			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			String clickURL = creative ["click_url"] as String;
			Application.OpenURL(clickURL);

			if(OnBannerWasClicked != null) OnBannerWasClicked();
		}	

	}
}