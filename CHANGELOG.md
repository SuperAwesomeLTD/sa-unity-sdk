CHANGELOG
=========

3.1.7
 - Updated the main SDK classes to correspond to the new iOS and Android Plugins (for iOS 5.3.15 and Android 5.3.9)
 - Removed some internal enums such as SABannerSize that were causing problems
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
