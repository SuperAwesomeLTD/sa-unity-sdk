---
title: Examples
description: Examples
---

# Simple example

The first example shows how you can add a banner ad in your app with just a few lines of code.

`activity_main.xml`

{% highlight xml %}
<tv.superawesome.sdk.publisher.SABannerAd
    android:id="@+id/mybanner"
    android:layout_width="match_parent"
    android:layout_height="100dp"/>
{% endhighlight %}

`MainActivity.java`

{% highlight java %}
// imports ...
import tv.superawesome.sdk.publisher.*;

public class MainActivity extends Activity {

    private SABannerAd banner = null;

    @Override
    protected void onCreate (Bundle savedInstanceState) {
        super.onCreate (savedInstanceState);
        setContentView (R.layout.activity_main);

        // get the banner
        bannerAd = (SABannerAd) findViewById (R.id.mybanner);

        // setup the banner
        bannerAd.disableParentalGate ();
        bannerAd.enableBumperPage ();

        // add a callback
        SAVideoAd.setListener(new SAInterface () {
            @Override
            public void onEvent(int placementId, SAEvent event) {
                if (event == SAEvent.adLoaded) {
                    bannerAd.play (MainActivity.this);
                }
            }
        });

        // start the loading process
        bannerAd.load (30471);
    }
}
{% endhighlight %}

## Complex example

This example shows how you can add different types of ads and make them respond to multiple callbacks.

{% highlight java %}
// imports ...
import tv.superawesome.sdk.publisher.*;

public class MainActivity extends Activity {

    // private SALoader class member
    private SALoader loader = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate (savedInstanceState);
        setContentView (R.layout.activity_main);

        // set app context
        SuperAwesome.getInstance ().setApplicationContext( getApplicationContext ());

        // get the banner
        bannerAd = (SABannerAd) findViewById (R.id.mybanner);

        // setup the banner
        bannerAd.enableParentalGate ();
        bannerAd.disableBumperPage();

        // and load it
        bannerAd.load (30471);

        // setup the video
        SAVideoAd.disableParentalGate ();
        SAVideoAd.enableBumperPage ();
        SAVideoAd.disableCloseButton ();

        // load
        SAVideoAd.load (30479, MainActivity.this);
        SAVideoAd.load (30480, MainActivity.this);
    }

    public void playBanner (View view) {
        if (banner.hasAdAvailable ()) {
            banner.play (MainActivity.this);
        }
    }

    public void playVideo1 (View view) {
        if (SAVideoAd.hasAdAvailable (30479)) {

            // do some last minute setup
            SAVideoAd.setOrientationLandscape ();

            // and play
            SAVideoAd.play (30479, MainActivity.this);
        }
    }

    public void playVideo2 (View view) {

        if (SAVideoAd.hasAdAvailable (30480)) {

            // do some last minute setup
            SAVideoAd.setOrientationAny ();

            // and play
            SAVideoAd.play (30480, MainActivity.this);
        }
    }
}
{% endhighlight %}