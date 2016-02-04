To complete integrating the SDK for Android, you'll need to follow the next steps (once):

#### Build the project for Android

To do this, click on `File > Build Settings` menu.
There, select the `Android` option and check the `Google Android Project` and `Development build` options.
Then, click on `Build` and save the new Android project on your drive.

![](img/IMG_08_AndroidBuild.png "Android Build")

#### Creating the setting file

Then, go to your new project folder:

```
cd /project_root

```

And in the root of the project create an empty file called `settings.gradle`.

![](img/IMG_08_AndroidProjectStructure.png "Project structure")

Then, using Android Studio, import your Unity Android project by selecting the .gradle file you just created (and following all instructions).

![](img/IMG_08_ImportingAndroid.png "Importing Android project")

#### Adding the SuperAwesome SDK via Gradle

Next, you'll need to add the AwesomeAds Android SDK. You can find more information [here](https://developers.superawesome.tv/docs/androidsdk?version=3) or you can follow the quick guide below:

Just include the following in your module's `build.gradle` file (usually the file under `MyApplication/app/`):

```
repositories {
    maven {
        url  "http://dl.bintray.com/sharkofmirkwood/maven"
    }
}

dependencies {
    compile 'tv.superawesome.sdk:sa-sdk:3.3.5@aar'
}

```

and click "Sync Task" when prompted.

![](img/IMG_09_GradleSetup.png "Setting up Android SDK with build.gradle")

If you'd want to install the SDK from a .jar archive, not through Gradle, follow the instructions [here](https://developers.superawesome.tv/docs/androidsdk/Getting%20Started/Adding%20the%20Library%20to%20Your%20Project%20-%20JAR%20Library?version=3).

#### Final setup

Finally, you'll need to do a small change to your default Unity Android manifest file.
Find the line

```
<meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="false" />

```

and set the value to `true`.
If you don't do this then banner ads won't be clickable on Android.