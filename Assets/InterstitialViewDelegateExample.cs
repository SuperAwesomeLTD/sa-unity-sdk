using UnityEngine;
using System.Collections;
using SuperAwesome;

public class InterstitialViewDelegateExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InterstitialView interstitial = this.GetComponentInChildren<InterstitialView> ();
		interstitial.OnInterstitialWasLoaded += OnLoad;
		interstitial.OnInterstitialWasClicked += OnClick;
		interstitial.OnInterstitialWasClosed += OnClosed;
		interstitial.OnInterstitialError += OnError;
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

	void OnError()
	{
		Debug.Log ("Interstitial Error");
	}
}
