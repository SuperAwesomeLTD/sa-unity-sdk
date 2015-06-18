using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class InterstitialView : MonoBehaviour {

		public String placementID = "Your Placement ID";
		public bool testMode = true;

		public delegate void InterstitialWasLoadedHandler();
		public event InterstitialWasLoadedHandler OnInterstitialWasLoaded;
		public delegate void InterstitialWasClickedHandler();
		public event InterstitialWasClickedHandler OnInterstitialWasClicked;
		public delegate void InterstitialWasClosedHandler();
		public event InterstitialWasClosedHandler OnInterstitialWasClosed;
		public delegate void InterstitialErrorHandler();
		public event InterstitialErrorHandler OnInterstitialError;

		private Button interstitialButton;
		private Button closeButton;
		private Image backgroundImage;
		private Ad ad;
		private bool display;
		private bool isReady;

		// Use this for initialization
		void Start () {			
			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach(Button button in buttons)
			{
				if(button.name == "CloseButton") this.closeButton = button;
				if(button.name == "Button") this.interstitialButton = button;
			}

			this.closeButton.onClick.AddListener (() => OnClose ());
			this.interstitialButton.onClick.AddListener (() => OnClick ());
			this.backgroundImage = gameObject.GetComponent<Image> ();

			this.display = true;
			Hide ();
			Load ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Show()
		{
			Align ();
			this.backgroundImage.enabled = true;
			this.interstitialButton.gameObject.SetActive (true);
			this.closeButton.gameObject.SetActive (true);
		}
		
		private void Hide()
		{
			this.interstitialButton.gameObject.SetActive (false);
			this.closeButton.gameObject.SetActive (false);
			this.backgroundImage.enabled = false;
		}

		private void Align()
		{
			float x = Screen.width / 2;
			float y = Screen.height / 2;
			interstitialButton.transform.position = new Vector3 (x, y, transform.position.z);
			x += this.ad.width/2;
			y += this.ad.height/2;
			closeButton.transform.position = new Vector3 (x, y, transform.position.z);
		}

		private void Load()
		{
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.testMode, this.OnAdLoaded));
		}

		private void OnAdLoaded(Ad ad)
		{
			this.ad = ad;
			if (this.ad == null)
			{
				if(OnInterstitialError != null) OnInterstitialError();
			}else{
				StartCoroutine (this.ad.LoadImage (this.OnTextureLoaded));
			}
		}

		public void OnTextureLoaded() {
			
			//Resize button using its RectTransform component
			this.interstitialButton.image.rectTransform.sizeDelta = new Vector2 (this.ad.width, this.ad.height);
			
			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(this.ad.texture, new Rect(0, 0, this.ad.texture.width, this.ad.texture.height), new Vector2(0.5f,0.5f));
			this.interstitialButton.image.sprite = sprite;
			
			if(OnInterstitialWasLoaded != null) OnInterstitialWasLoaded();
		}

		private void OnClick()
		{
			Application.OpenURL(this.ad.clickURL);
			
			if(OnInterstitialWasClicked != null) OnInterstitialWasClicked();
		}


		private void OnClose()
		{
			Hide ();

			if(OnInterstitialWasClosed != null) OnInterstitialWasClosed();
		}
	}
}