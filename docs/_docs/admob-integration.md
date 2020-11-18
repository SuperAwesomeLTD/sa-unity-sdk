---
title: Integrate with AdMob
description: Integrate with AdMob
---

# Integrate with AdMob

If you already have AdMob ads serving in your app, but want to integrate AwesomeAds as well, without having to directly use the Unity Publisher SDK, you can follow the steps below:

## Add the AdMob plugin

Download the latest `SuperAwesomeAdMob` Publisher SDK: {% include doc.html name="Releases" path="releases" %}

## Setup AdMob Mediation Groups

For Android platform use: [Android Guidelines](https://superawesomeltd.github.io/sa-mobile-sdk-android/docs/admob-integration#setup-admob-mediation-groups)

For iOS platform use: [iOS Guidelines](https://superawesomeltd.github.io/sa-mobile-sdk-ios/docs/admob-integration#setup-admob-mediation-groups)


## Implement Ads

Please follow the official [AdMob Unity](https://developers.google.com/admob/unity/quick-start) guidelines to implement the ads. 

## Use Resolver to Download Dependencies

SuperAwesomeAdMob SDK internally uses the [External Dependency Manager](https://github.com/googlesamples/unity-jar-resolver).

In Unity Editor, go to:

On the top menu -> Assets > External Dependency Manager -> Android Resolver -> Force Resolve

EDM will download the neccesarry Android dependencies.

## Customisation

Make your customisations first:

```c#
SAVideoAd.enableCloseButton();
SAVideoAd.enableParentalGate();
SAVideoAd.enableBackButton();
```

After you make the changes for the settings for `Video` or `Interstitial` ads, call `applySettings()` right before calling the `show()` method:

```c#
SAVideoAd.applySettings();
this.rewardedAd.Show();
```
