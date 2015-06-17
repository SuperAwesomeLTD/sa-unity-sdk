using UnityEngine;
using System.Collections;
using SuperAwesome;

public class BannerViewDelegateExample : MonoBehaviour {

	void OnEnable()
	{
		BannerView banner = this.GetComponent<BannerView>();
		banner.OnBannerWasLoaded += OnLoad;
		banner.OnBannerWasClicked += OnClick;
	}
	
	
	void OnDisable()
	{
		BannerView banner = this.GetComponent<BannerView>();
		banner.OnBannerWasLoaded -= OnLoad;
		banner.OnBannerWasClicked -= OnClick;
	}

	void OnLoad()
	{
		Debug.Log ("Banner Loaded");
	}
	
	
	void OnClick()
	{
		Debug.Log ("Banner Clicked");
	}
}
