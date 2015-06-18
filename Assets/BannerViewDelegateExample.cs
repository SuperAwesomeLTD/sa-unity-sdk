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
		bannerView.OnBannerError -= OnError;
	}

	void OnLoad()
	{
		Debug.Log ("Banner Loaded; starting new delayed load (currently " +
		           Time.realtimeSinceStartup + " seconds after startup)");
		StartCoroutine (this.bannerView.DelayedLoad ());
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
