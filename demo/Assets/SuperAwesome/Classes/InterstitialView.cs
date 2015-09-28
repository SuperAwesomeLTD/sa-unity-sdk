using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace SuperAwesome
{
	public class InterstitialView : MonoBehaviour {
		
		public String placementID = "Your Placement ID";
		public bool testMode = true;
		public bool openInstantly = true;
		
		public bool display { get; set; }
		public bool isReady { get; set; }
		public bool isParentalGateEnabled = false;
		
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
		
		private Button interstitialButton;
		private Button closeButton;
		private Button padlockButton;
		private Image backgroundImage;
		private Ad ad;
		private GameObject backgroundPlane;

		private float timescale;
		
		// Use this for initialization
		void Start () {
			
			Button[] buttons = this.GetComponentsInChildren<Button>();
			foreach (Button button in buttons)
			{
				if (button.name == "Button") this.interstitialButton = button;
				if (button.name == "CloseButton") this.closeButton = button;
				if (button.name == "PadlockButton") this.padlockButton = button; 
			}
			
			this.closeButton.onClick.AddListener (() => OnClose ());
			this.interstitialButton.onClick.AddListener (() => OnClick ());
			this.padlockButton.onClick.AddListener (() => OnPadlockClick ());
			this.backgroundImage = gameObject.GetComponent<Image> ();
			
			this.Hide ();
			if (this.openInstantly) {
				this.Show ();
			}
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetKey(KeyCode.X)) this.Show ();	//Remove when ready for production; use to test re-opening of the interstitial
		}
		
		public void Show()
		{
			if (!isReady)
			{
				this.display = true;
			} else {
				StartCoroutine(EventManager.Instance.LogViewableImpression(this.ad));
				this.createFakeBackground();

				Align ();
				this.backgroundImage.enabled = true;
				this.interstitialButton.gameObject.SetActive (true);
				this.closeButton.gameObject.SetActive (true);
				this.padlockButton.gameObject.SetActive(true);
				
				if (OnInterstitialWasOpened != null)
					OnInterstitialWasOpened ();
			}
		}
		
		private void Hide()
		{
			this.interstitialButton.gameObject.SetActive (false);
			this.closeButton.gameObject.SetActive (false);
			this.padlockButton.gameObject.SetActive (false);
			this.backgroundImage.enabled = false;
			this.display = false;
			this.Load ();
		}
		
		private void Align()
		{
			float x_mid = Screen.width / 2;
			float y_mid = Screen.height / 2;
			interstitialButton.transform.position = new Vector3 (x_mid, y_mid, transform.position.z);
		}
		
		private void Load()
		{
			this.isReady = false;
			StartCoroutine(SuperAwesome.instance.adManager.getAd (this.placementID, this.testMode, this.OnAdLoaded));
		}
		
		private void OnAdLoaded(Ad ad)
		{
			this.ad = ad;
			if (this.ad == null)
			{
//				StartCoroutine(EventManager.Instance.LogAdFailed(this.ad));
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
		
		private void OnClose()
		{
			destroyFakeBackground ();
			Hide ();
			if(OnInterstitialWasClosed != null) OnInterstitialWasClosed();
		}
		
		// part background
		private void createFakeBackground() {
			// make the background plane (the one that keeps clicks out)
			Vector3 cameraPos = Camera.main.transform.position;
			Vector3 cubePos = cameraPos;
			cubePos.z += 1.0f;
			Quaternion cameraRot = Camera.main.transform.rotation;
			
//			Material m = new Material(Shader.Find("Mobile/Diffuse"));
//			m.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
			
			backgroundPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
//			backgroundPlane.GetComponent<Renderer>().material = m;
//			backgroundPlane.renderer.materials[0] = m;
			backgroundPlane.renderer.materials [0].color = new Color (1, 0, 0);
//			backgroundPlane.GetComponent<Renderer> ().material.color = Color.red;
			backgroundPlane.transform.localScale = new Vector3 (100, 100, 1);
			backgroundPlane.transform.rotation = cameraRot;
			backgroundPlane.transform.position = cubePos;
		}
		
		private void destroyFakeBackground() {
			// just destroy the component
			Destroy (this.backgroundPlane);
		}
	}
	
}