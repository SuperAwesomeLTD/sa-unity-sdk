---
title: Ad callbacks
description: Ad callbacks
---

# Ad callbacks

Banner ads, interstitials and video ads all send a number of callbacks to inform you of important lifecycle events.

{% highlight java %}
SAVideoAd.setListener (new SAInterface() {
    @Override
    public void onEvent(int placementId, SAEvent event) {
        switch (event) {
            case adLoaded:
                // called when an ad has finished loading
                break;
            case adEmpty:
                // called when the request was successful but the server returned no ad
                break;
            case adFailedToLoad:
                // called when an ad could not be loaded
                break;
            case adShown:
                // called when an ad is first shown
                break;
            case adFailedToShow:
                // called when an ad fails to show
                break;
            case adClicked:
                // called when an ad is clicked
                break;
            case adEnded:
                // called when a video ad has ended playing (but hasn't yet closed)
                break;
            case adClosed:
                // called when a fullscreen ad is closed
                break;
        }
    }
});
{% endhighlight %}
