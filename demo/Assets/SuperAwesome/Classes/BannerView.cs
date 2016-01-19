// imports
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.EventSystems;

// part of the SuperaAwesome namespace
namespace SuperAwesome {

	// define and implement the BannerView class
	public class BannerView : MonoBehaviour {

		////////////////////////////////////////////////////
		/// Member variables
		////////////////////////////////////////////////////

		// 1. banner specific and general variables 
		public enum 	Layout { Manual, Top, Bottom };
		public Layout 	layout = Layout.Manual;
		public String	placementID = "Your Placement ID";
		public int 		refreshAfterSeconds = 30;
		public bool 	testMode = true;
		public bool 	isParentalGateEnabled = false;
		private Ad 		ad;

		// 2. subviews of the banner ad
		private Button	button;
		private Button 	padlockButton;
		private Image 	image;

		// 3. delegate functions
		public delegate void BannerWasLoadedHandler();
		public event BannerWasLoadedHandler OnBannerWasLoaded;
		public delegate void BannerWasClickedHandler();
		public event BannerWasClickedHandler OnBannerWasClicked;
		public delegate void BannerErrorHandler();
		public event BannerErrorHandler OnBannerError;

		////////////////////////////////////////////////////
		/// Initialise Banner Ad
		////////////////////////////////////////////////////

		void Start () {

			// 1. Get components
			this.image = this.GetComponent<Image>();
			
			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach (Button button in buttons) {
				if (button.name == "Banner") this.button = button;
				if (button.name == "PadlockButton") this.padlockButton = button; 
			}

			// 2. Add listeners
			this.button.onClick.AddListener (() => OnClick ());
			this.padlockButton.onClick.AddListener (() => OnPadlockClick ());

			// 3. Before all loading, hide the banner ad
			Hide ();

			// 4. Start trying to load the ad
			Load ();
		}

		void Update () {
			// Not implemented
		}

		////////////////////////////////////////////////////
		/// Load the banner ad
		////////////////////////////////////////////////////

		public void Load() {
			// call to async coroutine with callback OnAdLoaded
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.testMode, this.OnAdLoaded));
		}
		
		public IEnumerator DelayedLoad() {
			if (this.ad == null) {
				yield return null;
			} else {
				yield return new WaitForSeconds (this.refreshAfterSeconds);
				this.Load ();
			}
		}

		public void OnAdLoaded(Ad ad) {
			this.ad = ad;

			// if ad is null, goto Error case
			if (this.ad == null) {
				Debug.Log("Banner Ad could not be loaded");
				if(OnBannerError != null) OnBannerError();
			} 
			// if ad is not null, go forward and call an async texture laod function
			else {
				Debug.Log("Banner Ad was loaded OK");
				StartCoroutine (this.ad.LoadImage (this.OnTextureLoaded));
			}
		}
		
		public void OnTextureLoaded() {
			// 1. Log ad ready
			StartCoroutine (EventManager.Instance.LogViewableImpression (this.ad));
			
			// 2. Resize button using its RectTransform component
			this.button.image.rectTransform.sizeDelta = new Vector2 (this.ad.width, this.ad.height);
			
			// 3. Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(this.ad.texture, new Rect(0, 0, this.ad.texture.width, this.ad.texture.height), new Vector2(0.5f,0.5f));
			this.button.image.sprite = sprite;

			// 4. call the show function 
			Show ();

			// 5. Call delegate function
			if(OnBannerWasLoaded != null) OnBannerWasLoaded();
		}

		////////////////////////////////////////////////////
		/// Display the banner ad
		////////////////////////////////////////////////////

		private void Show() {

			// 1. do some final setup for the ad
			this.image.color = Color.white;
			this.padlockButton.gameObject.SetActive (!this.ad.fallback);

			// 2. call the aligh function
			this.Align ();
		}

		private void Align() {

			// 1. decide on correct position
			float x, y;
			switch (this.layout) {
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

			// 2. act on it for the whole banner
			transform.position = new Vector3 (x, y, transform.position.z);
		}

		private void Hide() {

			this.image.color = Color.clear;
			this.padlockButton.gameObject.SetActive (false);
		}
		
		////////////////////////////////////////////////////
		/// Interact with the banner ad
		////////////////////////////////////////////////////

		public void OnPadlockClick() {

		}
		
		private void OnClick() {


			this.goDirectlyToAdURL();
		}
		
		public void goDirectlyToAdURL(){
			StartCoroutine(EventManager.Instance.LogClick (this.ad));
			Application.OpenURL(this.ad.clickURL);
			if(OnBannerWasClicked != null) OnBannerWasClicked();
		}
	}
}