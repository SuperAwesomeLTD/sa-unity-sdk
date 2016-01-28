Each ad type, Banner, Interstitial or Video, sends out callbacks for major ad lifecycle events.
Registering for events follows the same steps as registering for loading callbacks.

Declare that ExampleScript implements the necessary interfaces

```
public class ExampleScript : MonoBehaviour , SALoaderInterface , SAAdInterface , SAVideoAdInterface , SAParentalGateInterface {

```

Íet the delegates:

Banner ads:

```
myBanner.adDelegate = this;
myBanner.parentalGateDelegate = this;

```

Interstitial ads:

```
myInterstitial.adDelegate = this;
myInterstitial.parentalGateDelegate = this;

```

Video Ads:

```
myVideoAd.adDelegate = this;
myVideoAd.parentalGateDelegate = this;
myVideoAd.videoAdDelegate = this;

```

In the ExampleScript class, implement all the necessary functions:

For `SAAdInterface`:

```
public void adWasShown(int placementId) {
	// called when an ad is succesfully shown
}

public void adFailedToShow(int placementId) {
	// called when an ad failed to show for multiple reasons
	// like there was no ad associated with a placement, etc
}

public void adWasClosed(int placementId) {
	// called when a video or interstitial ad (which are fullscreen) 
	// is closed by the user
}

public void adWasClicked(int placementId) {
	// called when a user clicks on an Ad
}

public void adHasIncorrectPlacement(int placementId) {
	// called when an ad has an incorrect placement type; for example:
	// if the ad server returns a video ad for a banner type placement
	// or a rich media ad trying to display in a video ad
}

```

For `SAParentalGateInterface`:

```
public void parentalGateWasCanceled(int placementId) {
	// called when a user cancels a parental gate
}
		
public void parentalGateWasFailed(int placementId) {
	// called when a user typed an incorrect number into the parental gate
	// and it failed
}

public void parentalGateWasSucceded(int placementId) {
	// called when a user typed the correct number into the parental gate
	// and went forward
}

```

For `SAVideoAdInterface`:

```
public void adStarted(int placementId){
	// called when an Ad in the video VAST XML started to play
}
		
public void videoStarted(int placementId){
	// called when a video inside an Ad started to play
}
		
public void videoReachedFirstQuartile(int placementId){
	// when the same video reached 1/4 of playing time
}
		
public void videoReachedMidpoint(int placementId){
	// when the same video reached 1/2 of playing time
}
		
public void videoReachedThirdQuartile(int placementId){
	// when the same video reached 3/4 of playing time
}
		
public void videoEnded(int placementId){
	// when the same video reached the end
}
		
public void adEnded(int placementId){
	// when the ad started in adStarted callback ended
}
		
public void allAdsEnded(int placementId){
	// when all ads in the VAST tag ended playing
}

```