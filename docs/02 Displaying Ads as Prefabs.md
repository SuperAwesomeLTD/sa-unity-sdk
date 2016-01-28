Once you've succesfully integrated the Unity SDK (and the native iOS or Android ones), the simplest way to actually load and diplay ads in your app is through prefabs.

In the Project Explorer panel in the Unity interface, find the `Assets` folder and the `SuperAwesome` subfolder.
There you'll find three prefabs called:

* SABannerAd
* SAInterstitialAd 
* SAVideoAd

![](img/IMG_10_Prefab1.png "Viewing prefabs")

Drag and drop any of then into your scene on the UI Layer.

![](img/IMG_11_BanenrPrefab.png "Adding the banner")

You'll see the prefab has a default rectangular shape and has a default texture associated. Don't worry about this, it will only display in editing mode, not playing mode.

Each prefab has a number of associated parameters, that shouw up when clicking on them.
In summary, this is what they're named and what they influence:

| Parameter | Description | Banner | Interstitial | Video |
|-------------------------|
| Placement Id | Specify the ID of the placement you want to load an ad for | Available | Available | Available | 
| Test Mode Enabled | If this placement is in test mode or not | Available | Available | Available |
| Is Parental Gate Enabled | If when clicking on the ad, a user will activate a Parental Gate | Available | Available | Available |
| Should Auto Start | Always set to True for Prefabs | Available | Available | Available |
| Position | Only for banners; Can be Top or Bottom | Available | N/A | N/A |
| Size | Only for banners; Can be 320x50, 300x50, 728x90, 300x250 | Available | N/A | N/A |
| Should Automatically Close At End | Only for video; specifies if the ad should close when it ends; should be set to true for prefabs | N/A | N/A | Available |
| Should Show Close Button | Only for video; specifies if the close button should be visible | N/A | N/A | Available |

In order to make the ad actually display, select the Prefab you just created and change it's parameters:

![](img/IMG_12_BannerPrefabConfig.png "Banner config")

For a banner you'll need to specify:

* Placement Id
* Test Mode Enabled
* Is Parental Gate Enabled
* Should Auto Start - always set to True for Prefabs
* Position - either Top or Bottom
* Size - can be 320x50, 300x50, 728x90, 300x250

For interstitial ads you'll need:

![](img/IMG_13_InterstitialPrefabConfig.png "Interstitial config")

* Placement Id
* Test Mode Enabled
* Is Parental Gate Enabled
* Should Auto Start

and for video ads you'll need:

![](img/IMG_14_VideoPrefab_Config.png "Video config")

* Placement Id
* Test Mode Enabled
* Is Parental Gate Enabled
* Should Auto Start
* Should Automatically Close At End - dictates if the video should close on it's own once it's ended. For prefabs this should always be set to true.
* Should Show Close Button - whether the fullscreen video ad has a close button in the top right corner.