### Exporting a Unity project to Android

The first step to creating an iOS project is exporting it from Unity.
To do so, go to File -> Build Settings and from the pop-up window select `Android`. Then select:
  * Google Android Project
  * Development Build (optional)

![](img/export_android.png "Exporting project to Android")

Then press on the `Player settings` button and make sure:
  * Minimum API level Android Ice Cream Sandwish 4.0

![](img/export_android2.png "Configuring Player settings for Android")

Next, press on `Build` and save your project in a separate folder, for example `UnityAndroidSADemo`. The folder will be populated by Android Studio files.

After Unity has exported your Android project, add an empty `settings.gradle` file to the project root (the folder which contains the `src` directory). Then you can import the project using Android Studio. When given the option to select the project to import, choose the `settings.gradle` file and Android Studio will import the project, filling the settings file and creating the additional required Gradle files.

![](img/import_project.png "Importing your Unity project to Android Studio")

### Adding specific SuperAwesome functionality

Up until this point, these are the standard steps required to export any Unity project to Android.
In order to add advanced functionality to AwesomeAds, the next step is to integrate the Android SuperAwesome SDK into your current Android Project.

First make sure you have the SDK downloaded - you can get it here: https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android and you can save it anywhere on your computer.

In Android Studio, go to "File > New > Import Module..." and, when prompted, select the 'superawesomesdk' directory from the files you just downloaded. This will import the SDK files into your project.

The last step is to add a dependency on the SDK for your project. The simplest way to do this is to open the `build.gradle` file in the `app` folder of your project (not the global project one), find the `dependencies` section and add the following line:
```
compile project(':bee7androidsdkgamewall')
compile project(':superawesomesdk')
```

It can also be done by adding a 'Module dependency' in the settings for your project and choosing the 'superawesomesdk' module. Whichever way you do it, your `build.gradle` file should have a dependencies section like this:

```
dependencies {
    compile files('libs/unity-classes.jar')
    compile project(':bee7androidsdkgamewall')
    compile project(':superawesomesdk')
}
```

Additionally you may want to add two lines to your global `build.gradle` file, in the dependencies part:

```
classpath 'com.github.dcendents:android-maven-plugin:1.2'
classpath 'com.jfrog.bintray.gradle:gradle-bintray-plugin:1.3.1'

```

Now your Android project will be ready to use and any calls to the native SDK from your Unity project will work as expected.