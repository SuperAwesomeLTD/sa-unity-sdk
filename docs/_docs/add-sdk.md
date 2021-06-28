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

![import-step]({{ site.baseurl }}/assets/img/IMG_02_Import.png){:class="img-responsive"}

Select all the files, and click Import. If all goes well you should have a series of new folders and files in your Assets directory.

![assets-step]({{ site.baseurl }}/assets/img/IMG_03_Assets.png){:class="img-responsive"}

Once you’ve integrated the SuperAwesome SDK, you can access it by:

```c#
using tv.superawesome.sdk.publisher;
```
## Additional steps for Android builds

### Unity build settings
As of Unity 2020 you must enable the `Export Project` option in the Android build settings and `Export` the project prior to building.

### Android Manifest
The flag `unityplayer.ForwardNativeEventsToDalvik` should be enabled. Open the `AndroidManifest.xml` file and add the following line
```xml
<meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="true" />
```

### Kotlin support

{% include alert.html type="warning" title="Warning" content="Kotlin support must be added to the Android project as the SDK relies on it. Without Kotlin, your app may install and load without issue, but the SDK will fail to display an ad." %}

To add Kotlin, you'll first need to include the following entries in the list of repositories in the `android` gradle file

```gradle
repositories {
  ...
  maven { url "https://maven.google.com" } // New repo
  maven { url "https://plugins.gradle.org/m2/" } // New repo
  google()
  jcenter()
  ...
}
```

Then you'll need to add a new `classpath` entry

```gradle
dependencies {
  ...
  classpath "org.jetbrains.kotlin:kotlin-gradle-plugin:1.5.10"
  ...
}
```

Include the Kotlin plugins in the `android.launcher` gradle file

```gradle
apply plugin: 'kotlin-android'
apply plugin: 'kotlin-android-extensions'
```

<!-- {% include alert.html type="warning" title="Warning" content="Please remember that for Android you also need to add <strong>Google Play Services</strong> and an <strong>App Compat</strong> library. These are needed for correct viewability metrics." %}

```gradle
dependencies {
    implementation 'com.android.support:appcompat-v7:+'
    implementation 'com.google.android.gms:play-services-ads:+'
}
``` -->

## Additional steps for iOS builds

{% include alert.html type="warning" title="Warning" content="SDK version 8.0.5 or older are incompatible with Xcode's New Build System. Please switch the build system from New to Legacy by going to File -> Project Settings and changing the Build System drop down." %}

<!-- Set swift version to 4.2, if not already set

![image-title-here]({{ site.baseurl }}/assets/img/IMG_ADD_1.png){:class="img-responsive"} -->

In the `Build Settings` for `Unity-iPhone target`, ensure `@executable_path/Frameworks` exists in runpaths...

![update-runpaths-step]({{ site.baseurl }}/assets/img/IMG_ADD_2.png){:class="img-responsive"}

... set C lang dialect to `gnu99`, if not already set...

![set-c-lang-dialect-step]({{ site.baseurl }}/assets/img/IMG_ADD_4.png){:class="img-responsive"}

... enable modules (C and Obj-C), if not already enabled.

![enable-modules-step]({{ site.baseurl }}/assets/img/IMG_ADD_5.png){:class="img-responsive"}

Make sure `SuperAwesome.framework` is in the `Embed Frameworks` list and `Code Sign on Copy` is selected.

In the `Build Phases` for `Unity-iPhone target`, find the `Embed Frameworks` options and add the `SuperAwesome` framework.

![embed-frameworks-step]({{ site.baseurl }}/assets/img/IMG_ADD_7.png){:class="img-responsive"}

## Remove Unsupported Architectures for App Store

SuperAwesome SDK framework for Unity contains both `arm` and `x86_64` code which allows running it on a physical device and on a simulator at the same time.

However to publish your app to the App Store the unused architectures(simulator) should be removed from the binary before publishing.

### (Option 1): Use `SuperAwesome SDK v7.2.12` and above

We have added a post build processor script which automatically removes Simulator architecture codes from the binary when uploading to App Store.

When you build the Unity project for the first time, the `SuperAwesome SDK` will a build phase called `SuperAwesome Strip Frameworks` into the build phases.

That script is only activated when the Xcode project is archived.


### (Option 2): Selecting supported architectures

In the `Target > Build Settings > Valid Architectures` menu, make sure `i386` and `x86_64` is not in the list.

![select-supported-architectures-step]({{ site.baseurl }}/assets/img/add-sdk-valid-archs.png){:class="img-responsive"}


{% include alert.html type="info" title="Note" content="After removing <strong>x86_64</strong> you’re no longer to run your app in the simulator. However, ideal solution would be to remove unused architectures only on `Release` mode." %}

### (Option 3 - Deprecated): Removing inactive code using a Run Script

<u><strong>Note:</strong> This option is now deprecated in favour of `Option 1`.</u>

After the frameworks are embedded to the binary, the unused code can be thinned using `lipo` command to create a new binary without simulator architecture codes.

1. Select your project file
2. Select the main target i.e. `Unity-iPhone`
3. Click <strong>Build Phases</strong>.
4. Click <strong>+</strong> (add) button on the top left corner, and select <strong>New Run Script Phase</strong>
    Note: A new run script added to the list
![image-title-here]({{ site.baseurl }}/assets/img/add-sdk-run-script-2.png){:class="img-responsive"}
5. Expand the new run script and copy the following script in it

```shell
# Signs a framework with the provided identity
code_sign() {
  # Use the current code_sign_identitiy
  echo "Code Signing $1 with Identity ${EXPANDED_CODE_SIGN_IDENTITY_NAME}"
  echo "/usr/bin/codesign --force --sign ${EXPANDED_CODE_SIGN_IDENTITY} --preserve-metadata=identifier,entitlements $1"
  /usr/bin/codesign --force --sign ${EXPANDED_CODE_SIGN_IDENTITY} --preserve-metadata=identifier,entitlements "$1"
}

echo "Stripping frameworks"
cd "${BUILT_PRODUCTS_DIR}/${FRAMEWORKS_FOLDER_PATH}"

for file in $(find . -type f -perm +111); do
  # Skip non-dynamic libraries
  if ! [[ "$(file "$file")" == *"dynamically linked shared library"* ]]; then
    continue
  fi
  # Get architectures for current file
  archs="$(lipo -info "${file}" | rev | cut -d ':' -f1 | rev)"
  stripped=""
  for arch in $archs; do
    if ! [[ "${VALID_ARCHS}" == *"$arch"* ]]; then
      # Strip non-valid architectures in-place
      lipo -remove "$arch" -output "$file" "$file" || exit 1
      stripped="$stripped $arch"
    fi
  done
  if [[ "$stripped" != "" ]]; then
    echo "Stripped $file of architectures:$stripped"
    if [ "${CODE_SIGNING_REQUIRED}" == "YES" ]; then
      code_sign "${file}"
    fi
  fi
done
echo "Stripping done"

```

<strong>Note:</strong> Make sure the newly added run script is at the end of the list in the build phases

<strong>Note 2:</strong> Select `Run script only when installing` to only use this script for archiving 