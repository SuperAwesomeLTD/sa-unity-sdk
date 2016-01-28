If you find that the prefab mechanism is a little too simple and does not afford enough flexibility, you can always load, create and display ads through code, using C#.

To do that you'll need to create a script and attach it to your Main Camera.

```
using UnityEngine;
using System.Collections;
// import SuperAwesome package
using SuperAwesome;

public class ExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

```

#### Sending the Loading message

In this example we'll use the `Start()` function to load ads.

```
void Start () {
	// these two lines setup the SuperAwesome environment
	// by default:
	// Test Mode is disabled
	// configuration is production (can be staging also, but not recommended) 
	SuperAwesome.SuperAwesome.instance.enableTestMode ();
	SuperAwesome.SuperAwesome.instance.setConfigurationProduction ();

	// and these three lines of code create an instance of a SALoader object
	// (which is used to preload Ads)
	// and call the loadAd() function, which takes one parameter - the placement Id
	SALoader loader = SALoader.createInstance ();
	loader.loadAd (5740);
}

```

#### Catching Loading events

Calling the `loadAd()` function initiates a call to the native iOS or Android code to asynchroniously load an ad.
Because it's async, we need a way to get the ad information back. We do this in three steps:

Declaring that our ExampleScript implements the `SALoaderInterface`:

```
public class ExampleScript : MonoBehaviour , SALoaderInterface {

```

Setting a delegate for the loader object:

```
loader.loaderDelegate = this;

```

In this case the delegate is the ExampleScript class itself, since it declares that it will implement the `SALoaderInterface` required functions. But it can be any object that implements them.

Finally, actually implementing the two functions specified by `SALoaderInterface`:

```
public class ExampleScript : MonoBehaviour, SALoaderInterface {

	void Start () {
		SuperAwesome.SuperAwesome.instance.enableTestMode ();
		SuperAwesome.SuperAwesome.instance.setConfigurationProduction ();

		SALoader loader = SALoader.createInstance ();
		loader.loaderDelegate = this;
		loader.loadAd (5740);
	}
	
	void Update () {
	
	}

	public void didLoadAd(SAAd adData) {
		// when this function is called, ad data is correctly loaded and
		// returned in the SAAd object
	}
	
	public void didFailToLoadAd(int placementId) {
		// when this function is called, ad data has not been loaded
		// correctly
	}
}

```

Once this is done, we've succesfully setup Awesome Ads, we've sent a message to the native iOS / Android SDK to load an ad and received back in Unity the requested Ad data.

The next chapters will deal with actually creating and displaying Banner, Interstitial and Video Ads.  