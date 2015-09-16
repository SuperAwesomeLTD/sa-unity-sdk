using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.EventSystems;

namespace SuperAwesome
{
	public class BannerView : MonoBehaviour {

		public enum Layout { Manual, Top, Bottom };

		public String placementID = "Your Placement ID";
		public Layout layout = Layout.Manual;
		public int refreshAfterSeconds = 30;
		public bool testMode = true;
		public bool isParentalGateEnabled = false;

		public delegate void BannerWasLoadedHandler();
		public event BannerWasLoadedHandler OnBannerWasLoaded;
		public delegate void BannerWasClickedHandler();
		public event BannerWasClickedHandler OnBannerWasClicked;
		public delegate void BannerErrorHandler();
		public event BannerErrorHandler OnBannerError;

		private Button button;
		private Button padlockButton;
		private Image image;
		private Ad ad;

		// Use this for initialization
		void Start ()
		{

			this.image = this.GetComponent<Image>();

			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach (Button button in buttons)
			{
				if (button.name == "Banner") this.button = button;
				if (button.name == "PadlockButton") this.padlockButton = button; 
			}

			this.button.onClick.AddListener (() => OnClick ());
			this.padlockButton.onClick.AddListener (() => OnClick ());

			Hide ();
			Load ();
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
		
		public void Load()
		{
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.testMode, this.OnAdLoaded));
		}
		
		public IEnumerator DelayedLoad()
		{
			if (this.ad == null)
			{
				yield return null;
			} else {
				yield return new WaitForSeconds (this.refreshAfterSeconds);
				this.Load ();
			}
		}

		public void OnAdLoaded(Ad ad)
		{
			this.ad = ad;
			if (this.ad == null)
			{
				if(OnBannerError != null) OnBannerError();
			} else {
				StartCoroutine (this.ad.LoadImage (this.OnTextureLoaded));
			}
		}

		public void OnTextureLoaded() {

			//Resize button using its RectTransform component
			this.button.image.rectTransform.sizeDelta = new Vector2 (this.ad.width, this.ad.height);
			
			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(this.ad.texture, new Rect(0, 0, this.ad.texture.width, this.ad.texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;
			
			Show ();
			
			if(OnBannerWasLoaded != null) OnBannerWasLoaded();

		}

		public void OnPadlockClick() {
			SABridge.showPadlockView ();
		}

		private void OnClick()
		{
			// case with parental gate
			if (this.isParentalGateEnabled == true) {
				SABridge.showParentalGate(this.name);
			} 
			// case no parental gate
			else {
				this.goDirectlyToAdURL();
			}
		}

		public void goDirectlyToAdURL(){
			Application.OpenURL(this.ad.clickURL);
			if(OnBannerWasClicked != null) OnBannerWasClicked();
		}
	}
}