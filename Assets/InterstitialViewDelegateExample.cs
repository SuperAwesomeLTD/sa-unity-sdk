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
		InterstitialView interstitial = this.GetComponentInChildren<InterstitialView> ();
		interstitial.isReady = true;

		// interstitial.display is true if Show() was called; it means we want to
		// display the ad when it's ready (ie. now). If the ad was ready at the time,
		// then it would have been displayed straight away.
		if (interstitial.display)
		{
			interstitial.Show ();
		}
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
