At the moment video ads are only available on iOS and Android. If you deploy to any other platform the video playing methods will have no effect.

The video ads make calls to native iOS and Android libraries through our SDKs, and as such you will need to include the native SDK into your project after Unity has built it. Instructions to do so vary depending on the platform, and can be found below: 

Android
-------
In the Player Settings pane set the minimum API level setting to Android 4.0 (API level 14) if it is lower.

When you export the Android project check the Google Android Project checkbox. This gives you an Android project you can modify in Android Studio, and allows you to import the SuperAwesome Android SDK before compiling for your device.

After Unity has exported your Android project, add an empty `settings.gradle` file to the project root (the folder which contains the `src` directory). Then you can import the project using Android Studio. When given the option to select the project to import, choose this file and Android Studio will import the project, filling the settings file and creating the additional required Gradle files.

![](img/import_project.png "Importing your Unity project to Android Studio")

Now you will have an Android Studio project set up using Gradle; with this you can import the SuperAwesome SDK for Android. First make sure you have the SDK downloaded - you can get it here: https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android/tree/2.0.1-beta-1

In Android Studio, go to "File > New > Import Module..." and, when prompted, select the 'superawesomesdk' directory from the files you just downloaded. This will import the SDK files into your project.

The last step is to add a dependency on the SDK for your project. The simplest way to do this is to open the `build.gradle` file for your project, find the `dependencies` section and add the following line:
```
compile project(':superawesomesdk')
```

It can also be done by adding a 'Module dependency' in the settings for your project and choosing the 'superawesomesdk' module. Whichever way you do it, your `build.gradle` file should have a dependencies section like this:
```
dependencies {
    compile files('libs/unity-classes.jar')
    compile project(':superawesomesdk')
}
```

Now your Android project will be ready to use and any calls to the native SDK from your Unity project will work as expected.


iOS
---
In the Player Settings pane set the Target iOS Version setting to 6.0 if it is lower.

After you have built your iOS project, you have to manually add the SuperAwesome SDK. To do this, follow the instructions in the iOS SDK documentation:

https://developers.superawesome.tv/docs/iossdk_v2/Getting%20Started/Integrating%20the%20SDK

After the CocoaPod dependency has been added, open the `xcworkspace` file in your project's directory (not the `xcodeproj` file) with Xcode. You have to make some changes to the default Unity build configuration, as the CocoaPods settings need to be propagated in the build target but won't have done so since Unity has already set these values.

![](img/xcode_build_settings.png "Find the Build Settings and change the values mentioned below.")

You will need to search for each of `OTHER_LDFLAGS`, `OTHER_CFLAGS` and `HEADER_SEARCH_PATHS`, double-click on them, and add `$(inherited)` to the list of existing values for these settings. You likely will have also received a message when running `pod update`, warning you to do this.

Once this is done your iOS project will be ready to use and any calls to the native SDK from your Unity project will work as expected.
