---
title: Integrate with MoPub
description: Integrate with MoPub
---

# Integrate with MoPub

If you already have MoPub ads serving in your app, but want to integrate AwesomeAds as well, without having to directly use the Android Publisher SDK, you can follow the steps below:

## Add the MoPub plugin

You can either change your module’s `build.gradle` file (usually the file under MyApplication/app/) to the following format:

{% highlight gradle %}
repositories {
    maven {
        url  "http://dl.bintray.com/gabrielcoman/maven"
    }
}

dependencies {
    # add the Android Publisher SDK
    compile 'tv.superawesome.sdk.publisher:superawesome:7.2.7'

    # add MoPub plugin
    compile 'tv.superawesome.sdk.publisher:samopub:7.2.7'
}
{% endhighlight %}

## Setup MoPub Custom Networks

Login to the MoPub dashboard using your preferred account.

From here forward the tutorial assumes you have an Android app with three ad units setup in MoPub; one banner, one interstitial ad and one rewarded video ad:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_07_MoPub_0.png){:class="img-responsive"}

From your MoPub admin interface you should create a `New Network`

![image-title-here]({{ site.baseurl }}/assets/img/IMG_07_MoPub_1.png){:class="img-responsive"}

From the next menu, select `Custom Native Network`

![image-title-here]({{ site.baseurl }}/assets/img/IMG_07_MoPub_2.png){:class="img-responsive"}

You’ll be taken to a new page. Here select the title of the new network

![image-title-here]({{ site.baseurl }}/assets/img/IMG_07_MoPub_3.png){:class="img-responsive"}

And assign custom inventory details for Banner, Interstitial and Video ads:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_07_MoPub_4.png){:class="img-responsive"}

Notice that the custom event classes required by MoPub are:
 - for Banner Ads: com.mobub.sa.mobileads.SAMoPubBannerCustomEvent
 - for Interstitial Ads: com.mobub.sa.mobileads.SAMoPubInterstitialCustomEvent
 - for Rewarded Video Ads: com.mobub.sa.mobileads.SAMoPubVideoCustomEvent

Finally, you can tell MoPub what AwesomeAds ads to load and how to display them by filling out the custom event class data field with a JSON similar to this:

{% highlight json %}
{
    "placementId": 30473,
    "isTestEnabled": true or false,
    "isParentalGateEnabled": true or false,
    "orientation": "ANY" or "PORTRAIT" or "LANDSCAPE",
    "shouldShowCloseButton": false or false,
    "shouldAutomaticallyCloseAtEnd": true or false,
    "shouldShowSmallClickButton": true or false
}
{% endhighlight %}

## Implement Ads

Once the previous steps are done, you can add MoPub banners, interstitials and rewarded video ads just as you normally would:

{% highlight java %}
// create banner ad
MoPubView banner = (MoPubView) findViewById(R.id.BannerID);
banner.setAdUnitId("_AD_UNIT_ID_");
banner.loadAd();

// create interstitial
MoPubInterstitial interstitial = new MoPubInterstitial(this, "_AD_UNIT_ID_");
interstitial.load();

// create video
MoPubRewardedVideos.initializeRewardedVideo(this);
MoPubRewardedVideos.loadRewardedVideo("_AD_UNIT_ID_");
{% endhighlight %}

Since the previously created custom events will run on these ads, and the Android Publisher SDK is integrated alongside the MoPub plugin, you should start seeing ads playing.