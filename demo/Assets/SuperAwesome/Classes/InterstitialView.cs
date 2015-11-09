// imports
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

// part of the SuperAwesome namespace
namespace SuperAwesome {

	// define and implement the Interstitial Ad
	public class InterstitialView : MonoBehaviour {

		////////////////////////////////////////////////////
		/// Member variables
		////////////////////////////////////////////////////

		// 1. ad and interstitial specific variables
		public String	placementID = "Your Placement ID";
		public bool 	testMode = true;
		public bool 	openInstantly = true;
		public bool 	display { get; set; }
		public bool 	isReady { get; set; }
		public bool 	isParentalGateEnabled = false;
		private Ad 		ad;

		// 2. subviews of the interstitial ad
		private Button 	interstitialButton;
		private Button 	closeButton;
		private Button 	padlockButton;
		private Image 	backgroundImage;
		private 
			GameObject	backgroundPlane;

		// 3. delegate functions
		public delegate void InterstitialWasOpenedHandler();
		public event InterstitialWasOpenedHandler OnInterstitialWasOpened;
		public delegate void InterstitialWasLoadedHandler();
		public event InterstitialWasLoadedHandler OnInterstitialWasLoaded;
		public delegate void InterstitialWasClickedHandler();
		public event InterstitialWasClickedHandler OnInterstitialWasClicked;
		public delegate void InterstitialWasClosedHandler();
		public event InterstitialWasClosedHandler OnInterstitialWasClosed;
		public delegate void InterstitialErrorHandler();
		public event InterstitialErrorHandler OnInterstitialError;

		////////////////////////////////////////////////////
		/// Initialise Interstitial Ad
		////////////////////////////////////////////////////

		void Start () {

			// 1. get a reference to interstitial ad subviews
			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach (Button button in buttons) {
				if (button.name == "Button") this.interstitialButton = button;
				if (button.name == "CloseButton") this.closeButton = button;
				if (button.name == "PadlockButton") this.padlockButton = button; 
			}

			// 2. add actions to the each subview
			this.closeButton.onClick.AddListener (() => OnClose ());
			this.interstitialButton.onClick.AddListener (() => OnClick ());
			this.padlockButton.onClick.AddListener (() => OnPadlockClick ());
			this.backgroundImage = gameObject.GetComponent<Image> ();

			// 3. perform actual init of the interstitial scene
			this.Hide ();
			this.Load ();

			// 4. also show, if openInstantly = true
			if (this.openInstantly) {
				this.Show ();
			}
		}
		
		void Update () {
			// not implemented
		}

		////////////////////////////////////////////////////
		/// Load the Interstitial ad
		////////////////////////////////////////////////////

		private void Load() {
			// set ready = false and start the async coroutine, with callback when ad is loaded
			this.isReady = false;
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.testMode, this.OnAdLoaded));
		}
		
		private void OnAdLoaded(Ad ad) {
			this.ad = ad;

			// if some type of ad error
			if (this.ad == null){
				Debug.Log("Could not load interstitial");
				if(OnInterstitialError != null) {
					OnInterstitialError();
				}
			}
			// if all is OK, call async coroutine to load the texture
			else {
				Debug.Log("Interstitial loaded OK");
				StartCoroutine (this.ad.LoadImage (this.OnTextureLoaded));
			}
		}
		
		public void OnTextureLoaded() {
			
			// 1. Resize button using its RectTransform component
			this.interstitialButton.image.rectTransform.sizeDelta = new Vector2 (this.ad.width, this.ad.height);
			
			// 2. Create a new sprite with the texture and apply it to the button
			Sprite sprite = Sprite.Create(this.ad.texture, new Rect(0, 0, this.ad.texture.width, this.ad.texture.height), new Vector2(0.5f,0.5f));
			this.interstitialButton.image.sprite = sprite;

			// 3. call delegate method
			if(OnInterstitialWasLoaded != null) OnInterstitialWasLoaded();
		}

		////////////////////////////////////////////////////
		/// Display the Interstitial ad
		////////////////////////////////////////////////////

		public void Show() {

			if (!isReady) {
				this.display = true;
			} 
			// if ad is ready
			else {
				// 1. log viewable impression
				StartCoroutine(EventManager.Instance.LogViewableImpression(this.ad));

				// 2. create the fake background
				createFakeBackground ();

				// 3. align the interstitial
				Align ();

				// 4. do some more enabling of interstitial subviews
				this.backgroundImage.enabled = true;
				this.interstitialButton.gameObject.SetActive (true);
				this.closeButton.gameObject.SetActive (true);
				this.padlockButton.gameObject.SetActive(!this.ad.fallback);

				// 5. call delegate method
				if (OnInterstitialWasOpened != null) OnInterstitialWasOpened ();
			}
		}

		private void Align() {
			// align the interstitial's overlay button
			float x_mid = Screen.width / 2;
			float y_mid = Screen.height / 2;
			interstitialButton.transform.position = new Vector3 (x_mid, y_mid, transform.position.z);
		}

		private void Hide () {
			// destroy the background
			destroyFakeBackground ();

			// hide all the things when call to hide (basically opposite of Show()
			this.interstitialButton.gameObject.SetActive (false);
			this.closeButton.gameObject.SetActive (false);
			this.padlockButton.gameObject.SetActive (false);
			this.backgroundImage.enabled = false;
			this.display = false;
		}

		private void createFakeBackground() {
			// Get the camera pos and such, because we're creating the 
//			// background "From literally scratch"
//			Vector3 cameraPos = Camera.main.transform.position;
//			Vector3 cubePos = cameraPos;
//			cubePos.z += 1.0f;
//			Quaternion cameraRot = Camera.main.transform.rotation;
//
//			// init the Interstitials subview
//			backgroundPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
//
////			backgroundPlane.transform.renderer.material.color = new Color (0.0f, 1.0f, 0.0f, 0.5f); // Color.red;
////			Color color = backgroundPlane.transform.renderer.material.color;
////			color.a = 0.5f;
////			backgroundPlane.transform.renderer.material.color = color;
////			backgroundPlane.renderer.materials [0].shader = Shader.Find( "Transparent/Diffuse");
//
//			backgroundPlane.renderer.materials [0].color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
//			backgroundPlane.transform.localScale = new Vector3 (100, 100, 1);
//			backgroundPlane.transform.rotation = cameraRot;
//			backgroundPlane.transform.position = cubePos;
////			backgroundPlane.gameObject.o
//
//
////			backgroundPlane.AddComponent<Button> ();
//
			Button brn = gameObject.AddComponent<Button> ();
			brn.onClick.AddListener (() => OnClose ());
//			backgroundPlane.AddComponent(brn);
		}

		////////////////////////////////////////////////////
		/// Interact with the Interstitial ad
		////////////////////////////////////////////////////

		private void OnClick()
		{
#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
			// case with parental gate
			if (this.isParentalGateEnabled == true) {
				SABridge.showParentalGate(this.name, this.ad.placementId, this.ad.creativeId, this.ad.lineItemId);
			} 
			// case no parental gate
			else {
				this.goDirectlyToAdURL();
			}
#else
			this.goDirectlyToAdURL();
#endif
		}
		
		public void OnPadlockClick() {
#if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
			if (!this.ad.fallback) {
				SABridge.showPadlockView ();
			}
#endif
		}
		
		public void goDirectlyToAdURL(){
			StartCoroutine(EventManager.Instance.LogClick (this.ad));
			Application.OpenURL(this.ad.clickURL);
			if(OnInterstitialWasClicked != null) OnInterstitialWasClicked();
		}

		////////////////////////////////////////////////////
		/// Close the Interstitial ad
		////////////////////////////////////////////////////

		private void OnClose () {

			// 1. call hide (with all that entails: destroy background, etc)
			Hide ();

			// 2. call delegate
			if(OnInterstitialWasClosed != null) OnInterstitialWasClosed();
		}

		private void destroyFakeBackground() {
			// just destroy the component
			Destroy (this.backgroundPlane);
		}
	}
	
}