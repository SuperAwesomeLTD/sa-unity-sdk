SuperAwesome Unity SDK
==========================

[![GitHub tag](https://img.shields.io/github/tag/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![GitHub contributors](https://img.shields.io/github/contributors/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![license](https://img.shields.io/github/license/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![Language](https://img.shields.io/badge/language-csharp-f48041.svg?style=flat)]() [![Platform](https://img.shields.io/badge/platform-android-lightgrey.svg)]() [![Platform](https://img.shields.io/badge/platform-ios-lightgrey.svg)]()

Unity SDK Integration Documentation: [SuperAwesome Developer Portal](https://superawesomeltd.github.io/sa-unity-sdk/).


Setup Build Environment
=======================
- Install [Unity Hub](https://unity3d.com/get-unity/download)
- Install [XCode](https://apps.apple.com/us/app/xcode/id497799835?mt=12)
- Install [Android Studio](https://developer.android.com/studio)
- Install Cocoapods `brew install cocoapods`
- In Unity Hub download 2019 LTS and 2020 LTS
- In Android Studio -> SDK Manager -> Download Android 29
- Set up your IOS Developer Profile in XCode
`Xcode -> Preferences -> Account -> Add Apple ID w/ Password -> Verify with Device* -> Add a development certificate`  
The account information will be shared internally via LastPass.
- Run: `sudo xcode-select -s /Applications/Xcode.app/Contents/Developer`
- Restart your terminal
- Add this environment variable to your shell profile, replacing the path with the location of your Android SDK. This path can be found in the Android SDK Manager when installing SDK versions.
`export ANDROID_SDK_ROOT="path/to/Android/sdk"`

Building a new version
======================
Run `./build.sh` to generate a new `.unitypackage` in `scripts/output/`.

Optional params to the build script:
- `-v`  version; tag the build with a version number. Default is empty.
- `-b`  branch; use a different branch of the ios and android sdk. Default is `master`
- `-p`  platform; one of `android`, `ios` or `all`. Default is `all`.

Using your iOS build in Unity
=============================
See the integration documentation for Unity setup

Using your Android build in Unity
=================================
See the integration documentation for Unity setup
