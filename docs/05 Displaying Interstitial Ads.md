Interstitial ads follow the same pattern:

```
public void didLoadAd(SAAd adData) {
	SAInterstitialAd iad = SAInterstitialAd.createInstance();
	iad.setAd(adData);
	iad.isParentalGateEnabled = true;
	iad.play();
}

```