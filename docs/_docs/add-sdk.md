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

## Remove Unsupported Architectures for App Store

SuperAwesome SDK framework for Unity contains both `arm` and `x86_64` code which allows running it on a physical device and on a simulator at the same time.

However to publish your app to the App Store the unused architectures(simulator) should be removed from the binary before publishing.

<strong>(Option 1): Selecting supported architectures</strong>

In the `Target > Build Settings > Valid Architectures` menu, make sure `i386` and `x86_64` is not in the list.

![image-title-here]({{ site.baseurl }}/assets/img/add-sdk-valid-archs.png){:class="img-responsive"}


{% include alert.html type="info" title="Note" content="After removing <strong>x86_64</strong> you’re no longer to run your app in the simulator. However, ideal solution would be to remove unused architectures only on `Release` mode." %}

<strong>(Option 2): Removing inactive code using a Run Script</strong>

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