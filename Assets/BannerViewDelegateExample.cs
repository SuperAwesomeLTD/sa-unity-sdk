using UnityEngine;
using System.Collections;
using SuperAwesome;

public class BannerViewDelegateExample : MonoBehaviour {

	private BannerView bannerView;

	void OnEnable()
	{
		bannerView = this.GetComponentInChildren<BannerView> ();
		bannerView.OnBannerWasLoaded += OnLoad;
		bannerView.OnBannerWasClicked += OnClick;
		bannerView.OnBannerError += OnError;
	}
	
	
	void OnDisable()
	{
		bannerView.OnBannerWasLoaded -= OnLoad;
		bannerView.OnBannerWasClicked -= OnClick;
	}

	void OnLoad()
	{
		Debug.Log ("Banner Loaded");
	}
	
	
	void OnClick()
	{
		Debug.Log ("Banner Clicked");
	}

	void OnError()
	{
		Debug.Log ("Banner Error");
	}
}
