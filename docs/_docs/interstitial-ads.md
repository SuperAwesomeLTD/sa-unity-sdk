---
title: Interstitial Ads
description: Interstitial Ads
---

# Interstitial Ads

The following code block sets up an interstitial ad and loads it:

```c#
public class MainScript : MonoBehaviour {

    // initialization
    void Start () {

        // set configuration production
        SAInterstitialAd.setConfigurationProduction ();

        // to display test ads
        SAInterstitialAd.enableTestMode ();

        // lock orientation to portrait or landscape
        SAInterstitialAd.setOrientationPortrait ();

        // enable or disable the android back button
        SAInterstitialAd.enableBackButton ();

        // start loading ad data for a placement
        SAInterstitialAd.load (30473);
    }
}
```

Once you’ve loaded an ad, you can also display it:

```c#
public void playInterstitial () {

    // check if ad is loaded
    if (SAInterstitialAd.hasAdAvailable (30473)) {

        // display the ad
        SAInterstitialAd.play (30473);
    }
}
```

These are the default values:

| Parameter | Value |
|-----|-----|
| Configuration | Production |
| Test mode | Disabled |
| Orientation | Any | 
| Back button | Enabled |
