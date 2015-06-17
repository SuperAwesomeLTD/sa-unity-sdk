using UnityEngine;
using System.Collections;
using SuperAwesome;

public class InterstitialViewDelegateExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InterstitialView banner = this.GetComponentInChildren<InterstitialView> ();
		banner.OnInterstitialWasLoaded += OnLoad;
		banner.OnInterstitialWasClicked += OnClick;
		banner.OnInterstitialWasClosed += OnClosed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLoad()
	{
		Debug.Log ("Interstitial Loaded");
		InterstitialView banner = this.GetComponentInChildren<InterstitialView> ();
		banner.Show ();
	}
	
	
	void OnClick()
	{
		Debug.Log ("Interstitial Clicked");
	}

	void OnClosed()
	{
		Debug.Log ("Interstitial Closed");
	}
}
