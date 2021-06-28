SuperAwesome Unity SDK
==========================

[![GitHub tag](https://img.shields.io/github/tag/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![GitHub contributors](https://img.shields.io/github/contributors/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![license](https://img.shields.io/github/license/SuperAwesomeLTD/sa-unity-sdk.svg)]() [![Language](https://img.shields.io/badge/language-csharp-f48041.svg?style=flat)]() [![Platform](https://img.shields.io/badge/platform-android-lightgrey.svg)]() [![Platform](https://img.shields.io/badge/platform-ios-lightgrey.svg)]()

For more information check out the [SuperAwesome Developer Portal](https://superawesomeltd.github.io/sa-unity-sdk/).


New version
===========
Run `./build.sh -v x.y.z` to generate a new `.unitypackage` in `scripts/output/`.

Setup
=====
Add this environment variable to your shell profile, replacing the path with the location of your Android SDK:
`export ANDROID_SDK_ROOT="path/to/Android/sdk"`

Setup iOS
=========
To finish the setup once you build the Unity project as an iOS application, you also need to add the following frameworks into:
`Build Phases` -> `Embed Framework`.

![ios setup](img/ios-setup-new.png "Finalise iOS Setup")

Setup Android
=============
To finish the setup once you build the Unity project as an Android applicatiom, you will need to add `Kotlin` support as part of your `gradle` setup, if you haven't already added it.

Add this repository to the list of repositoies

`maven { url "https://plugins.gradle.org/m2/" }`

![android setup 1](img/android-setup-1.png "Finalise Androi Setup Part 1")

Then add this classpath

`classpath "org.jetbrains.kotlin:kotlin-gradle-plugin:$kotlinVersion"`

![android setup 2](img/android-setup-2.png "Finalise Androi Setup Part 2")

The finally add the `kotlin plugins`

![android setup 3](img/android-setup-3.png "Finalise Androi Setup Part 2")
