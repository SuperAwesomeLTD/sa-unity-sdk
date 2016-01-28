If you find that the prefab mechanism is a little too simple and does not afford enough flexibility, you can always load, create and display ads through code, using C#.

To do that you'll need to create a script and attach it to your Main Camera.

```
using UnityEngine;
using System.Collections;

public class ExampleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

```

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
	loader.loaderDelegate = this;
	loader.loadAd (23000);
}

```