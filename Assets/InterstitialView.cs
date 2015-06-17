using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class InterstitialView : MonoBehaviour {

		public String placementID = "Your Placement ID";

		public delegate void InterstitialWasLoadedHandler();
		public event InterstitialWasLoadedHandler OnInterstitialWasLoaded;
		public delegate void InterstitialWasClickedHandler();
		public event InterstitialWasClickedHandler OnInterstitialWasClicked;
		public delegate void InterstitialWasClosedHandler();
		public event InterstitialWasClosedHandler OnInterstitialWasClosed;

		private Button interstitialButton;
		private Button closeButton;
		private Image image;
		private Dictionary<string, object> ad;
		private Int64 width;
		private Int64 height;

		// Use this for initialization
		void Start () {
			this.image = this.GetComponent<Image>();
			
			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach(Button button in buttons)
			{
				if(button.name == "CloseButton") this.closeButton = button;
				if(button.name == "Button") this.interstitialButton = button;
			}

			this.closeButton.onClick.AddListener (() => OnClose ());
			this.interstitialButton.onClick.AddListener (() => OnClick ());

			Hide ();
			StartCoroutine(Load());
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Show()
		{
			Align ();
			this.interstitialButton.gameObject.SetActive (true);
			this.closeButton.gameObject.SetActive (true);
		}
		
		private void Hide()
		{
			this.interstitialButton.gameObject.SetActive (false);
			this.closeButton.gameObject.SetActive (false);
		}

		private void Align()
		{
			float x = Screen.width / 2;
			float y = Screen.height / 2;
			interstitialButton.transform.position = new Vector3 (x, y, transform.position.z);
			x += this.width/2;
			y += this.height/2;
			closeButton.transform.position = new Vector3 (x, y, transform.position.z);
		}

		private IEnumerator Load()
		{
			this.ad = SuperAwesome.instance.adManager.getAd (this.placementID);
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			Dictionary<string, object> details = creative ["details"] as Dictionary<string, object>;
			string imgurl = (string) details["image"];
			this.width = (Int64) details["width"];
			this.height = (Int64) details["height"];
			
			WWW image = new WWW(imgurl);
			yield return image;
			Texture2D texture = image.texture;
		
			//Resize button using its RectTransform component
			this.interstitialButton.image.rectTransform.sizeDelta = new Vector2 (this.width, this.height);

			//Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
			this.interstitialButton.image.sprite = sprite;
			
			if(OnInterstitialWasLoaded != null) OnInterstitialWasLoaded();
		}

		private void OnClick()
		{
			Dictionary<string, object> creative = this.ad ["creative"] as Dictionary<string, object>;
			String clickURL = creative ["click_url"] as String;
			Application.OpenURL(clickURL);
			
			if(OnInterstitialWasClosed != null) OnInterstitialWasClosed();
		}


		private void OnClose()
		{
			Hide ();

			if(OnInterstitialWasClosed != null) OnInterstitialWasClosed();
		}
	}
}