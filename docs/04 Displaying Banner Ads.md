Once ad data for a Banner type placement is loaded, one way to display the actual banner on screen is to change the `didLoadAd(SAAd adData)` function and add the following code:

```
public void didLoadAd(SAAd adData) {
	SABannerAd banner = SABannerAd.createInstance ();
	banner.setAd (adData);
	banner.position = SABannerAd.BannerPosition.BOTTOM;
	banner.size = SABannerAd.BannerSize.BANNER_320_50;
	banner.isParentalGateEnabled = false;
	banner.play ();
}

```

The three most important lines of code here are:

```
SABannerAd banner = SABannerAd.createInstance ();

```

Which creates a new SABanner object. This is equivalent to dragging & dropping a banner prefab in your scene.

```
banner.setAd (adData);

```

This basically tells the new `banner` object what ad data to use to display the ad contents on screen.
adData is a `SAAd` type object. That's an custom object made to hold SuperAwesome Ad related data such as width, height, click URL, etc.
	
```
banner.play();

```

Finally, call the `play()` function to actually display the ad on screen.

Note that if you save the `SAAd adData` reference like this:

```
public void didLoadAd(SAAd adData) {
	// where myAd is a previously declared SAAd type object
	this.myAd = adData;
}

```

you can then create and play a banner ad anytime - once a button is clicked, at the end of a game level, etc.
