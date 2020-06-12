---
title: Integrate with AdMob
description: Integrate with AdMob
---

# Integrate with AdMob

If you already have AdMob ads serving in your app, but want to integrate AwesomeAds as well, without having to directly use the Android Publisher SDK, you can follow the steps below:

## Add the AdMob plugin

You can either change your module’s `build.gradle` file (usually the file under MyApplication/app/) to the following format:

{% highlight gradle %}
repositories {
    maven {
        url  "http://dl.bintray.com/gabrielcoman/maven"
    }
}

dependencies {
    # add the Android Publisher SDK
    compile 'tv.superawesome.sdk.publisher:superawesome:{{ site.latest_version }}'

    # add MoPub plugin
    compile 'tv.superawesome.sdk.publisher:saadmob:{{ site.latest_version }}'
}
{% endhighlight %}

## Setup AdMob Mediation Groups

Login to the AdMob dashboard using your preferred account.

From here forward the tutorial assumes you have an Android app with three ad units setup in AdMob; one banner, one interstitial ad and one rewarded video ad:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_1.png){:class="img-responsive"}

Then, in the `Mediation` menu, create a new Mediation Group:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_2.png){:class="img-responsive"}

Next, fill in the necessary details:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_3.png){:class="img-responsive"}

and add your app’s banner `Ad Unit` as target:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_4.png){:class="img-responsive"}

Then, in the `Ad Sources` panel, add a new `Custom Event`:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_5.png){:class="img-responsive"}

and, as well, customise it:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_6.png){:class="img-responsive"}

and finally set the AwesomeAds custom event class name as Class Name `tv.superawesome.plugins.publisher.admob.SAAdMobBannerCustomEvent` and the parameter as your Placement ID:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_08_AdMob_7.png){:class="img-responsive"}

Finally, save your changes. This will register `BannerCustomEvent` as a custom event running on your ad units from now on. You’ll have to repeat the same process for interstitial and rewarded video ads.

## Implement Ads

Once the previous steps are done, you can add AdMob banners, interstitials and rewarded video ads just as you normally would:

{% highlight java %}
// init app
MobileAds.initialize(this, "__YOUR_APP_ID__");

// add a new banner
AdView adView = (AdView) findViewById(R.id.AdViewID);
adView.loadAd(new AdRequest.Builder().build());

// add a new interstitial
InterstitialAd mInterstitialAd = new InterstitialAd(this);
mInterstitialAd.setAdUnitId("__YOUR_AD_ID__");
mInterstitialAd.loadAd(new AdRequest.Builder().build());

// add a new video
RewardedVideoAd mAd = MobileAds.getRewardedVideoAdInstance(this);
mAd.loadAd("__YOUR_AD_ID__", new AdRequest.Builder().build());
{% endhighlight %}

Since the previously created custom events will run on these ads, and AwesomeAds is integrated alongside the AdMob plugin, you should start seeing ads playing.

## Customise the Experience

Additionally, you can customize the experience of each ad unit.

 1. For banners:
{% highlight java %}
// build a new Bundle full of extra data
Bundle extras1 = SAAdMobExtras.extras()
    .setTestMode(false)
    .setParentalGate(false)
    .setTransparent(true)
    .build();

// register that bundle as an extra when creating the request
adView.loadAd(new AdRequest
    .Builder()
    .addCustomEventExtrasBundle(SAAdMobBannerCustomEvent.class, extras1)
    .build());
{% endhighlight %}

 2. For interstitials:
{% highlight java %}
// build a new Bundle full of extra data
Bundle extras2 = SAAdMobExtras.extras()
     .setTestMode(false)
     .setOrientation(SAOrientation.PORTRAIT)
     .setParentalGate(true)
     .build();


 // register that bundle as an extra when creating the request
 mInterstitialAd.loadAd(new AdRequest
     .Builder()
     .addCustomEventExtrasBundle(SAAdMobInterstitialCustomEvent.class, extras2)
     .build());
{% endhighlight %}
 3. For rewarded video:
{% highlight java %}
 // build a new Bundle full of extra data
 Bundle extras3 = SAAdMobExtras.extras()
       .setTestMode(false)
       .setParentalGate(false)
       .setOrientation(SAOrientation.LANDSCAPE)
       .setSmallClick(true)
       .setCloseAtEnd(true)
       .setCloseButton(true)
       .build();

 // register that bundle as an extra when creating the request
mAd.loadAd("__YOUR_AD_ID__", new AdRequest
       .Builder()
       .addNetworkExtrasBundle(SAAdMobVideoMediationAdapter.class, extras3)
       .build());
{% endhighlight %}

These parameters will be passed by the AdMob SDK to the AwesomeAds Plugin so that ads will display the way you want them to.