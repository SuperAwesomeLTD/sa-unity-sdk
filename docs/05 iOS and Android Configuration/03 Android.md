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
In order to add advanced functionality to AwesomeAds, the next step is to integrate the Android SuperAwesome SDK into your current Android Project following the [instructions here](https://developers.superawesome.tv/docs/androidsdk/Getting%20Started/Adding%20the%20Library%20to%20Your%20Project%20-%20AAR%20Library?version=3).

Now your Android project will be ready to use and any calls to the native SDK from your Unity project will work as expected.