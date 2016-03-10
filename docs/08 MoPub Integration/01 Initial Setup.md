If you already have MoPub ads serving in your app, but want to integrate SuperAwesome as well, without having to directly call the AwesomeAds SDK, you can follow the steps below:

#### Setting up MoPub in your Unity Project

The first thing you'll need to do, once you've created a new Unity project, is to head over to [MoPub's developer page](https://dev.twitter.com/mopub/unity) and download the latest [MoPub plugin](http://mopub-unity.mopub.com.s3.amazonaws.com/v3.8.0.zip).
This should contain a couple of .unitypackage files, chief among them **MoPubPlugin.unitypackage**. You'll need to import this into your Unity project.

![](img/IMG_15_MoPub1.png "unitypackage")

This should have created a new folder structure in your project:

![](img/IMG_15_MoPub2.png "folder structure")

#### Adding AwesomeAds adapters

Next you'll need to download the AwesomeAds MoPub adapters for iOS and Android and add them to your Unity project.

For iOS you'll need to download:

  * [SuperAwesomeBannerCustomEvent.h](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeBannerCustomEvent.h)
  * [SuperAwesomeBannerCustomEvent.m](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeBannerCustomEvent.m)
  * [SuperAwesomeInterstitialCustomEvent.h](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeInterstitialCustomEvent.h)
  * [SuperAwesomeInterstitialCustomEvent.m](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeInterstitialCustomEvent.m)
  * [SuperAwesomeRewardedVideoCustomEvent.h](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeRewardedVideoCustomEvent.h)
  * [SuperAwesomeRewardedVideoCustomEvent.m](https://raw.githubusercontent.com/SuperAwesomeLTD/sa-mobile-sdk-ios/master/Pod/Plugin/MoPub/SuperAwesomeRewardedVideoCustomEvent.m)

and place them into the `Assets/Editor/MoPub/NativeCode` folder.

![](img/IMG_15_MoPub3.png "added files")

For Android you'll just need to download [samopub.jar](https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android/blob/develop_v3/docs/res/samopub.jar?raw=true) and place it into `Assets/Plugins/Android` folder.

![](img/IMG_15_MoPub4.png "added jar")

To setup MoPub **AdUnits** in Unity, follow the guide [here](https://dev.twitter.com/mopub/unity).
This will detail Banner, Interstitial and Rewarded video ads - all of which are fully supported by AwesomeAds.

#### Adding ads

Summing up the previous detailed guide, use the following generic code to display:

  * Banner Ads

```
MoPub.createBanner( BANNER_ADUNIT_ID, MoPubAdPosition.BottomCenter );

```

  * Interstitial Ads

```
MoPub.requestInterstitialAd( INTERSTITIAL_ADUNIT_ID );
MoPub.showInterstitialAd( INTERSTITIAL_ADUNIT_ID );

```

  * Rewarded Video Ads

```
MoPub.initializeRewardedVideo();
MoPub.requestRewardedVideo( VIDEO_ADUNIT_ID );
MoPub.showRewardedVideo( VIDEO_ADUNIT_ID );  

```
