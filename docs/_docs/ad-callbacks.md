---
title: Ad callbacks
description: Ad callbacks
---

# Ad callbacks

Banner ads, interstitials and video ads all send a number of callbacks to inform you of important lifecycle events.

```c#
SAVideoAd.setCallback ((placementId, evt) => {
    switch (evt) {

    case SAEvent.adLoaded:
    // called when an ad has finished loading
    break;

    case SAEvent.adEmpty:
    // called when the request was successful but the server returned no ad
    break;

    case SAEvent.adFailedToLoad:
    // called when an ad could not be loaded
    break;

    case SAEvent.adShown:
    // called when an ad is first shown
    break;

    case SAEvent.adFailedToShow:
    // called when an ad fails to show
    break;

    case SAEvent.adClicked:
    // called when an ad is clicked
    break;

    case SAEvent.adEnded:
    // called when a video ad has ended playing (but hasn't yet closed)
    break;

    case SAEvent.adClosed:
    // called when a fullscreen ad is closed
    break;
    }
});
```
