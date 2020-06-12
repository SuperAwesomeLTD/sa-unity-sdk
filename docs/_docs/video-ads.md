---
title: Video Ads
description: Video Ads
---

# Video Ads

The following code block sets up a video ad and loads it:

{% highlight java %}
public class MainActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate (savedInstanceState);
        setContentView (R.layout.activity_main);

        // to display test ads
        SAVideoAd.enableTestMode ();

        // set configuration to production
        SAVideoAd.setConfigurationProduction ();

        // lock orientation to portrait or landscape
        SAVideoAd.setOrientationLandscape ();

        // enable or disable the android back button
        SAVideoAd.enableBackButton ();

        // enable or disable a close button
        SAVideoAd.enableCloseButton ();

        // enable or disable auto-closing at the end
        SAVideoAd.disableCloseAtEnd ();

        // make the whole video surface area clickable
        SAVideoAd.disableSmallClick ();

        // start loading ad data for a placement
        SAVideoAd.load (30479, MainActivity.this);
    }
}
{% endhighlight %}

Once you’ve loaded an ad, you can also display it:

{% highlight objective_c %}
public void onClick (View view) {

    // check if ad is loaded
    if (SAVideoAd.hasAdAvailable (30479)) {

        // display the ad
        SAVideoAd.play (30479, MainActivity.this);
    }
}
{% endhighlight %}

These are the default values:

| Parameter | Value |
|-----|-----|
| Configuration | Production |
| Test mode | Disabled |
| Orientation | Any | 
| Closes at end | True |
| Close button | Disabled |
| Small click button | Disabled | 
| Back button | Enabled |
