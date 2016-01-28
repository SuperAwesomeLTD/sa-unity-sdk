And Video Ads as well:

```
public void didLoadAd(SAAd adData) {
	SAVideoAd vad = SAVideoAd.createInstance();
	vad.setAd(adData);
	vad.isParentalGateEnabled = true;
	vad.shouldShowCloseButton = true;
	vad.shouldAutomaticallyCloseAtEnd = true;
	vad.play();
}

```