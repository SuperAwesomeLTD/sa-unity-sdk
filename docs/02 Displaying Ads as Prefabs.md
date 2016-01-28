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

Each prefab has a number of associated parameters, that show up when you select a prefab.
In summary, this is what they're named and what they influence:

| Parameter | Description | Banner | Interstitial | Video |
|-------------------------|
| Placement Id | Specify the ID of the placement you want to load an ad for | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) | 
| Test Mode Enabled | If this placement is in test mode or not | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) |
| Is Parental Gate Enabled | If when clicking on the ad, a user will activate a Parental Gate | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) |
| Should Auto Start | Always set to True for Prefabs | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) | ![](img/IMG_OK.png) |
| Position | Only for banners; Can be Top or Bottom | ![](img/IMG_OK.png) | ![](img/IMG_NOK.png) | ![](img/IMG_NOK.png) |
| Size | Only for banners; Can be 320x50, 300x50, 728x90, 300x250 | ![](img/IMG_OK.png) | ![](img/IMG_NOK.png) | ![](img/IMG_NOK.png) |
| Should Automatically Close At End | Only for video; specifies if the ad should close when it ends; should be set to true for prefabs | ![](img/IMG_NOK.png) | ![](img/IMG_NOK.png) | ![](img/IMG_OK.png) |
| Should Show Close Button | Only for video; specifies if the close button should be visible | ![](img/IMG_NOK.png) | ![](img/IMG_NOK.png) | ![](img/IMG_OK.png) |

Banners:

![](img/IMG_12_BannerPrefabConfig.png "Banner config")

Interstitials:

![](img/IMG_13_InterstitialPrefabConfig.png "Interstitial config")

Video Ads:

![](img/IMG_14_VideoPrefab_Config.png "Video config")
