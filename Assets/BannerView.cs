using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

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
		private Ad ad;

		// Use this for initialization
		void Start ()
		{
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.Load));
			this.image = this.GetComponent<Image>();

			this.button = this.GetComponent<Button>();
			this.button.onClick.AddListener (() => OnClick ());

			Hide ();
		}

		// Update is called once per frame
		void Update ()
		{

		}

		private void Show()
		{
			this.image.color = Color.white;
			this.Align ();
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
					y = this.ad.height / 2;
					break;
				case Layout.Top:
					x = Screen.width / 2;
					y = Screen.height - this.ad.height/2;
					break;
				default:
					x = transform.position.x;
					y = transform.position.y;
					break;
			}
			transform.position = new Vector3 (x, y, transform.position.z);
		}

		public void Load(Ad ad)
		{
			this.ad = ad;
			if (this.ad != null) {
				StartCoroutine (this.ad.LoadImage (this.UpdateTexture));
			}
		}

		public void UpdateTexture() {

			//Resize button using its RectTransform component
			this.button.image.rectTransform.sizeDelta = new Vector2 (this.ad.width, this.ad.height);
			
			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(this.ad.texture, new Rect(0, 0, this.ad.texture.width, this.ad.texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;
			
			Show ();
			
			if(OnBannerWasLoaded != null) OnBannerWasLoaded();

		}

		private void OnClick()
		{
			Application.OpenURL(this.ad.clickURL);

			if(OnBannerWasClicked != null) OnBannerWasClicked();
		}	

	}
}