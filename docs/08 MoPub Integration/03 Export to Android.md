With Android you'll need to follow a similar patter.

Start by actually exporting the project:

![](img/IMG_15_MoPub10.png "export to android")

Again, you'll miss the actual Android SDK from the newly created Android Studio project.

The simplest way of installing the AwesomeAds SDK in Android Studio is to download the AAR library through Gradle.

Just include the following in your module's `build.gradle` file (usually the file under `MyApplication/app/`):

```
repositories {
    maven {
        url  "http://dl.bintray.com/sharkofmirkwood/maven"
    }
}

dependencies {
    compile 'tv.superawesome.sdk:sa-sdk:3.5.0@aar'
    compile 'com.google.android.gms:play-services:8.4.0'
    compile files('libs/mopub-volley-1.1.0.jar')
    compile files('libs/samopub.jar')
    compile files('libs/unity-classes.jar')
    compile files('libs/MoPubPlugin.jar')
}

```

Notice that:
  * **unity-classes.jar** is provided by Unity.
  * **mopub-volley-1.1.0.jar** is provided by the MoPub Unity Plugin
  * **MoPubPlugin.jar** is provided by the MoPub Unity Plugin (and you sometimes have to copy it from the `moPub_lib/lib` folder of the exported project and add it as a library in Android Studio)
  * **samopub.jar** is the SuperAwesome MoPub adapters library you downloaded earlier
  * Otherwise we just register a SuperAwesome repository and add the library and google play services.   
