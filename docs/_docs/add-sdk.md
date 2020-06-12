---
title: Add the SDK through Gradle
description: Add the SDK through Gradle
---

# Add the SDK

The Unity Publisher SDK is built as an an Unity Package in order to work together with the Android and iOS native SDKs so that you can harness the full power of native components, such as video based on AVFoundation / VideoView technology, proper WebViews and a better fullscreen experience.

To begin integrating the SDK:

Download the latest Unity Publisher SDK: {% include doc.html name="Releases" path="releases" %}

This version will contain everything you need in order to load and display banner, interstitial and video ads.

You can then import it into your Unity project as a custom assets package. You should see an image similar to this:

![image-title-here]({{ site.baseurl }}/assets/img/IMG_02_Import.png){:class="img-responsive"}

Select all the files, and click Import. If all goes well you should have a series of new folders and files in your Assets directory.

![image-title-here]({{ site.baseurl }}/assets/img/IMG_03_Assets.png){:class="img-responsive"}

Once you’ve integrated the SuperAwesome SDK, you can access it by:

```c#
using tv.superawesome.sdk.publisher;
```
## Additional steps for Android builds

{% include alert.html type="warning" title="Warning" content="Please remember that for Android you also need to add <strong>Google Play Services</strong> and an <strong>App Compat</strong> library. These are needed for correct viewability metrics." %}

```gradle
dependencies {
    implementation 'com.android.support:appcompat-v7:+'
    implementation 'com.google.android.gms:play-services-ads:+'
}
```

{% include alert.html type="warning" title="Warning" content="When exporting for Android as an Android Studio project you’ll need to set the <strong>unityplayer.ForwardNativeEventsToDalvik</strong> entry to <strong>true</strong>" %}

```xml
<meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="true" />
```

## Additional steps for iOS builds

Set swift version to 4.2, if not already set

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_1.png){:class="img-responsive"}

Add runpaths @executable_path/Frameworks to runpaths

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_2.png){:class="img-responsive"}

Set C lang dialect to gnu99, if not already set

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_4.png){:class="img-responsive"}

Enable modules (C and Obj-C), if not already enabled

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_5.png){:class="img-responsive"}

Make sure `SuperAwesome.framework` is in the `Embed Frameworks` list and `Code Sign on Copy` is selected.

Go to:

`Unity-iPhone target -> Build Phases -> Embed Frameworks`

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_7.png){:class="img-responsive"}