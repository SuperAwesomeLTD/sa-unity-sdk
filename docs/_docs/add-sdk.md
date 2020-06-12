---
title: Add the SDK through Gradle
description: Add the SDK through Gradle
---

# Add the SDK through Gradle

The simplest way of adding the Android Publisher SDK to your Android Studio project is to download the AAR library through Gradle.

The first step is to include the following Maven repository in your module’s <strong>build.gradle</strong> file (usually the file under MyApplication/app/):

{% highlight gradle %}
repositories {
    maven { url  'http://dl.bintray.com/gabrielcoman/maven' }
    maven { url "http://dl.bintray.com/superawesome/SuperAwesomeSDK" }
}
{% endhighlight %}

Next you can to add the SDK as a dependency. This will contain everything you need in order to load and display banner, interstitial and video ads.

{% highlight gradle %}
dependencies {
    implementation 'tv.superawesome.sdk.publisher:superawesome: {{ site.latest_version }}'
}
{% endhighlight %}

{% include alert.html type="warning" title="Warning" content="Please remember to also add <strong>Google Play Services</strong> and an <strong>App Compat</strong> library. These are needed for correct viewability metrics." %}

{% highlight gradle %}
dependencies {
    implementation 'com.android.support:appcompat-v7:+'
    implementation 'com.google.android.gms:play-services-ads:+'
}
{% endhighlight %}

Once you’ve added the Android Publisher SDK, you can access all functionality by including:

{% highlight java %}
import tv.superawesome.sdk.publisher.*;
{% endhighlight %}