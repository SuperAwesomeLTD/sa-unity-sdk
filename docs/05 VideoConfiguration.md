At the moment video ads are only available on iOS and Android. If you deploy to an other platform the video playing methods will have no effect.

To use the native plugins for video you will first need to include the "Plugins" folder from the SDK in the "Assets" folder of your project. This includes native Android and iOS code that Unity can call to make use of features from these platforms.

Android
-------
In the Player Settings pane set the minimum API level setting to Android 2.3.3 (API level 10) if it is lower.

When you export the Android project check the Google Android Project checkbox. In the exported project the SuperAwesome SDK is automatically linked to your Unity project.

iOS
---
In the Player Settings pane set the Target iOS Version setting to 6.0 if it is lower.

After you have built your iOS project, you have to manually add the SuperAwesome SDK. To do this, follow the instructions in the iOS SDK documentation:

https://developers.superawesome.tv/docs/iossdk/Getting%20Started/Integrating%20the%20SDK

After the CocoaPod dependency has been added, open the xcworkspace file in your project's directory. You have to make some changes to the default Unity build configuration:

1. cocoapods settings needs to be propagated in the build target, but it doesn't since unity already set these values before.  Therefore you will have to manually add "$(inherited)" to all 3 build setting variables: OTHER_LDFLAGS, OTHER_CFLAGS, HEADER_SEARCH_PATHS

2. Unity did not set the build products path correctly, so you will need to correct it to look for where pods project will drop its libraries.  Set "per-configuration build products path" in build settings to this value: $(BUILD_DIR)/$(CONFIGURATION)$(EFFECTIVE_PLATFORM_NAME)