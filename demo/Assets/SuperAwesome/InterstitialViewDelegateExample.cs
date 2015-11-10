using UnityEngine;
using System.Collections;
using SuperAwesome;

public class InterstitialViewDelegateExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InterstitialView interstitial = this.GetComponentInChildren<InterstitialView> ();
		interstitial.OnInterstitialWasLoaded += OnLoad;
		interstitial.OnInterstitialWasOpened += OnOpened;
		interstitial.OnInterstitialWasClicked += OnClick;
		interstitial.OnInterstitialWasClosed += OnClosed;
		interstitial.OnInterstitialError += OnError;
		
		GetComponent<Canvas> ().sortingOrder = 1000;
	}

	void OnLoad()
	{
		Debug.Log ("Delegate - Interstitial Loaded");
		InterstitialView interstitial = this.GetComponentInChildren<InterstitialView> ();

		/*
		 * interstitial.display is true if Show() was called; it means we want to
		 * display the ad when it's ready (ie. now). If the ad was ready at the time,
		 * then it would have been displayed straight away.
		 */
		interstitial.isReady = true;
		if (interstitial.display)
		{
			interstitial.Show ();
		}
	}
	
	void OnOpened()
	{
		Debug.Log ("Delegate - Interstitial Shown");
	}
	
	void OnClick()
	{
		Debug.Log ("Delegate - Interstitial Clicked");
	}

	void OnClosed()
	{
		Debug.Log ("Delegate - Interstitial Closed");
	}

	void OnError()
	{
		Debug.Log ("Delegate - Interstitial Error");
	}
}
