Find your App ID and Placement ID on the [dashboard](http://dashboard.superawesome.tv).

You can then import the folders downloaded previously into your unity project (simply drag and drop). To keep everything separated we recommend you put the files inside "Assets/SuperAwesome/"- however the files will work as long as they are imported anywhere inside the "Assets" folder.

In order to display an ad, create a "GUITexture" GameObject, and add the "ad.cs" script to it (you can drag the script into the object in the Hierarchy pane).

There are some options you can configure before you can display an ad. Click on the GameObject you just created in the Hierarchy pane and find the ad script in the inspector.

The options are:
- App_id (Required): Your App ID from the Dashboard
- Placement_id (Required): Your Placement ID from the dashboard
- Hide (Default: false): If true the ad will be hidden until you decide to show it programmatically
- Closable (Default: false): If true a close button will appear at the top right of the ad to allow users to close the ad. This is recommended for interstitial ads so the users can continue to use your app after being shown the ad without you having to hide it manually
- Fix Placement (Default: Not Fixed): has 4 options:
	- Not Fixed: The ad will display wherever you place it
	- Top: The ad will be fixed to the top of the screen
	- Bottom: The ad will be fixed to the bottom of the screen
	- Center: The ad will be fixed to the middle of the screen

In order to show or hide an ad programmatically, you first need to import the ```superawesome``` namespace. You then need to select the GameObject and then the ad component of that object. You can then call the ```toggleAd();``` function to hide or show the ad.

Here is an example for a GameObject named "Interstitial" in C# that shows/hides the ad every 3 seconds.

```
using UnityEngine;
using System.Collections;
using superawesome; // Import the namespace

public class toggleAdTest : MonoBehaviour {

	public GameObject adObject;
	public ad adComponent;

	void Start () {
		adObject = GameObject.Find("Interstitial"); // Find the GameObject you wish to modify
		adComponent = (ad) adObject.GetComponent(typeof(ad)); // Assign a variable to the ad component

		StartCoroutine(toggleTest());
	}

	IEnumerator toggleTest() {
		yield return new WaitForSeconds(3);

		// check that the ad has loaded and can be displayed
		// Only matters if ad is currently hidden and so toggleAd() would show the ad
		if (adComponent.adReady) { 
			adComponent.toggleAd(); // Show or Hide the ad, depending on its current state
		}

		StartCoroutine(toggleTest());
	}
}
```