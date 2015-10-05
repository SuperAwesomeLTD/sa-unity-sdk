### Exporting a Unity project to iOS

The first step to creating an iOS project is exporting it from Unity.
To do so, go to File -> Build Settings and from the pop-up window select `iOS`. Then select:
  * Symlink Unity libraries
  * Development Build (optional)

![](img/export_ios.png "Exporting project to iOS")

Then press on the `Player settings` button and make sure:
  * Target iOS version is at least 6.0 
  * SDK version is either Simulator or Device, depending on your test mode

![](img/export_ios2.png "Configuring Player settings for iOS")

Next, press on `Build` and save your project in a separate folder, for example `UnityiOSSADemo`. The folder will be populated by XCode files, including the essential `Unity-iPhone.xcodeproj`.

### Adding specific SuperAwesome functionality

Up until this point, these are the standard steps required to export any Unity project to iOS.
In order to add advanced functionality to AwesomeAds, the next step is to integrate the iOS SuperAwesome SDK into your current iOS Project.

To to this, follow the standard instructions presented here: https://developers.superawesome.tv/docs/iossdk/Getting%20Started/Integrating%20the%20SDK?version=2

In short, the SuperAwesome iOS SDK resides on CocoaPodss. To install it, you need to add it just like you would any other CocoaPods library.

After the CocoaPod dependency has been added, open the `xcworkspace` file in your project's directory (not the `xcodeproj` file) with Xcode. You have to make some changes to the default Unity build configuration, as the CocoaPods settings need to be propagated in the build target but won't have done so since Unity has already set these values.

![](img/xcode_build_settings.png "Find the Build Settings and change the values mentioned below.")

You will need to search for each of `OTHER_LDFLAGS`, `OTHER_CFLAGS` and `HEADER_SEARCH_PATHS`, double-click on them, and add `$(inherited)` to the list of existing values for these settings. You likely will have also received a message when running `pod update`, warning you to do this.

Once this is done your iOS project will be ready to use and any calls to the native SDK from your Unity project will work as expected.