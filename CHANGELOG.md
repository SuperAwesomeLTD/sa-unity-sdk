CHANGELOG
=========

5.3.4
 - Updated the Unity SDK to work with the new Android (5.5.4) and iOS (5.5.4) SDKs that load data in webviews using a base url as well

5.3.3
 - Updated the Unity SDK to communicate with the new Android (5.5.3) and iOS (5.5.3) SDKs that add support for scrollable interstitial ads

5.3.2
 - Updated the Unity SDK to communicate with the new Android (5.5.2) and iOS (5.5.2) SDKs that add improvements to the modelspace and eventing systems
 - Fixed bitcode support for iOS

5.3.1
 - Minor improvement to SAInterstitialAd, SAVideoAd and SAAppWall to remove all prefabs.
 - Now you can pre-create any of those instances in Awake or Start
 - Removed calls to update

5.3.0
 - Updated the Unity SDK to communicate with the new Android (5.5.0) and iOS (5.5.0) SDKs
 - Added a separate SACPI class (singleton) to handle all CPI:
    - communicating with native methods
    - receiving a callback with a "success" parameter
    - also removed any reference of CPI in the SuperAwesome class (singleton)
 - Contains changes done in the 5.5.0 versions of the native SDKs:
    - reworked CPI
    - reworked events
    - reworked video player (for Android)
    - improvements & bug fixes

5.2.2
 - Updated the Unity SDK to communicate with the new Android (5.4.8) and iOS (5.4.1) SDK
 - Banner ads don't fire up an "adClosed" event on first load
 - The "adFailedToLoad" events get triggered correctly by the android video ad plugin (before the "adFailedToShow" event got triggered wrongly)
 - On iOS the "setOrientationLandscape" and "setOrientationPortrait"  methods will take into account the availabl
e orientations the app provides
 - The video ad close button will appear by default after 15 seconds of content playing

5.2.1
 - Updated the Unity SDK to communicate with the new Android (5.4.8) SDK
 - This will mean less files in the resulting .unitypackage import, since the 5.4.8 version removes all dependencies of xml layouts or drawables - all UI elements are now generated in code.
 - This also adds a new and improved video player that is much more versatile.

5.2.0
 - Updated the Unity SDK to communicate with the new iOS (5.4.0) and Android (5.4.0) SDKs
 - Those added support for a new WebPlayer that scales the ad using native code matrix manipulation
 - Also added support for the adEnded event, fired when a video ad ends (but not necessarily closes)
 - Added support for the adAlreadyLoaded event, fired when an Interstitial, Video or AppWall tries to
 load ad data for an already existing placement
 - Added support for the clickCounterUrl; that's been added as part of the native Ad Creative model class and is now fired when a user clicks an ad.

5.1.9
 - Updated the Unity SDK to communicate with the new iOS (5.3.17) and Android (5.3.13) SDKs.
 - Removed the staging CPI method from the SuperAwesome singleton class
 - All CPI calls now interact with both iOS and Android
 - All CPI calls now have a callback that lets the SDK user know if the Ad Server considered the inst
all event to be valid or not.
 - Refactored some navtive method calls.

5.1.8
 - Updated the Unity SDK to be able to override iOS & Android native SDK version & sdk type. This means that all requests from the Unity SDK will be labeled as "unity_x.y.z" instead of "android_x.y.z" or "ios_x.y.z", which in turn provide more accurate statistics for reporting.
 - Updated the method calls in each of the Unity-to-native methods so as to correspond to the new Android & iOS modular plugin structure.

5.1.7
 - Updated the main SDK classes to correspond to the new iOS and Android Plugins (for iOS 5.3.15 and Android 5.3.9)
 - Removed some internal enums such as SABannerSize that were causing problems. Now banners get sent the actual desired witdth & height (e.g. 320x50, 768x90, etc). This simplifies code a little bit and reduced dependency between SDKs (e.g. the Unity SDK and iOS SDKs don't need to know about the same enums)
 - Added default values in the SuperAwesome singleton - now all SDK customizable values are initialised from those values. These should be kept in line with the iOS and Android ones.

3.1.2
3.1.1
 - Add orientation lock

3.1.0
3.0.9
 - Updates to work w/ new versions of iOS & Android SDK

3.0.8
 - Update SAUnity.mm to work with Unity 5.3+

3.0.7
3.0.6
3.0.5
3.0.4
 - added the SAViewInterface to make the code more in line with iOS / Android practices

3.0.3
 - even tighter and more correct integration with ios & android sdks

3.0.0
 - Update to v3 Unity SDK - which is fully integrated with the iOS / Android native SDKs, and works only as such

2.2.3
 - Add video callbacks for Android

2.2.1
 - Fix some small video issues
 - Do a bit of code cleanup

2.1.1:
 - Updated banner & interstitials to work with v2 of the web API
 - Updated video ads to work
 - added Parental Gate (using native iOS / Android integration)
 - added padlock (using native iOS / Android integration)

1.1:
 - Unity Plugin for native iOS and Android SDKs
 - Added SuperAwesome.openVideoAd method to display video ads
