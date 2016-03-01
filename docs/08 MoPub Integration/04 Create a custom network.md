The final step is to create a Custom Native Network to interface with SuperAwesome, if you haven't already.

From your MoPub admin interface you should create a `New Network`

![](img/IMG_15_MoPub5.png "Adding a new Network")

Form the next menu, select `Custom Native Network`

![](img/IMG_15_MoPub6.png "Creating a Custom Native Network")

You'll be taken to a new page. Here select the title of the new network

![](img/IMG_15_MoPub7.png "Create the Super Awesome Network")

And assign custom inventory details for Banner, Interstitial and Video ads:

![](img/IMG_15_MoPub8.png "Setup custom inventory")

Custom Event Classes for iOS are:
  * for Banner Ads: `SuperAwesomeBannerCustomEvent`
  * for Interstitial Ads: `SuperAwesomeInterstitialCustomEvent`
  * for Rewarded Video Ads: `SuperAwesomeRewardedVideoCustomEvent`

Custom Event Classes for Android are:
  * for Banner Ads: `com.mopub.sa.mobileads.SuperAwesomeBannerCustomEvent`
  * for Interstitial Ads: `com.mopub.sa.mobileads.SuperAwesomeInterstitialCustomEvent`
  * for Rewarded Video Ads: `com.mopub.sa.mobileads.SuperAwesomeRewardedVideoCustomEvent`

Custom Event Data that is always required, and must be given in the form of  JSON:

```
{
	"placementId": 5692,
	"isTestEnabled": true,
	"isParentalGateEnabled": true
}

```

Optional Event Data for Rewarded Videos is:

```
{
  "shouldShowCloseButton": false,
  "shouldAutomaticallyCloseAtEnd": true
}

```

If you don't yet have a Placement ID for Awesome Ads, check out the [Getting Started / Registering Your App on the Dashboard](https://developers.superawesome.tv/docs/iossdk/Getting%20Started/Registering%20Your%20App%20on%20the%20Dashboard?version=4) section.
