To integrate the base Unity SDK into your app, first download the [SuperAwesome.unitypackage](https://github.com/SuperAwesomeLTD/sa-unity-sdk/blob/develop_v3/SuperAwesome.unitypackage?raw=true) file and import it into your Unity project as a custom assets package.

You should see an image similar to this:

![](img/IMG_02_Import.png "Importing the SuperAwesome.unitypackage")

Select all the files, and click Import. 
If all goes well you should have a series of new folders and files in your Assets folder.

![](img/IMG_03_Assets.png "The new assets folder")

The Unity SDK is essentially a thin layer of classes, functions and plugins used to communicate with the iOS or Android native SDKs. These two, depending on the case, will handle all the heavy lifting when it comes to actually load ad data. 
Rendering ads on screen falls also on the native SDKs for all three types of ads supported:
* Banner Ads
* Fullscreen or Interstitial Ads
* Preroll or Video Ads

This is so that your games or apps have the best support for rich media or third party tags.

In order to complete the SDK integration, skip to either the iOS or Android section of this documentation.