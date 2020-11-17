#!/bin/bash -ex

# prepare iOS
rm -rf build/ios/source
mkdir build/ios/source

cp -r source/ios/sa-mobile-sdk-ios/Pod/Plugin/Unity/* build/ios/source/
cp -r ios-pod/Rome/*.framework build/ios/source

project="SuperAwesomeSDK"
rm -rf build/unity/sa-unity-sdk

# clone the repo
source=sa-unity-sdk

cp -r source/unity/* build/unity/

mkdir build/unity/$source/demo/Assets/Plugins/
mkdir build/unity/$source/demo/Assets/Plugins/iOS
mkdir build/unity/$source/demo/Assets/Plugins/Android
mkdir build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib

# add android sources

androidManifest=build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib/"AndroidManifest.xml"
echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>" > $androidManifest
echo "<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\" package=\"tv.superawesome.sdk.publisher\">" >> $androidManifest
echo "<uses-permission android:name=\"android.permission.INTERNET\" />" >> $androidManifest
echo "<uses-permission android:name=\"android.permission.ACCESS_NETWORK_STATE\"/>" >> $androidManifest
echo "<application>" >> $androidManifest
echo "<activity android:name=\"tv.superawesome.sdk.publisher.SAVideoActivity\" android:label=\"SAFullscreenVideoAd\" android:theme=\"@android:style/Theme.Black.NoTitleBar.Fullscreen\" android:configChanges=\"keyboardHidden|orientation|screenSize\"/>" >> $androidManifest
echo "<activity android:name=\"tv.superawesome.sdk.publisher.SAInterstitialAd\" android:label=\"SAInterstitialAd\" android:theme=\"@android:style/Theme.Black.NoTitleBar.Fullscreen\" android:configChanges=\"keyboardHidden|orientation|screenSize\"/>" >> $androidManifest
echo "<activity android:name=\"tv.superawesome.lib.sabumperpage.SABumperPage\" android:label=\"SABumperPage\" android:configChanges=\"keyboardHidden|orientation|screenSize\" android:theme=\"@android:style/Theme.Holo.Dialog.NoActionBar\" android:excludeFromRecents=\"true\"/>" >> $androidManifest
echo "</application>" >> $androidManifest
echo "</manifest>" >> $androidManifest

projectProperties=build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib/"project.properties"
echo "# Project target." > $projectProperties
echo "target=android-11" >> $projectProperties
echo "android.library=true" >> $projectProperties

cp -r build/android/*.jar build/unity/$source/demo/Assets/Plugins/Android/

# add iOS sources

cp -r build/ios/source/ build/unity/$source/demo/Assets/Plugins/iOS

# build as UnityPackage
/Applications/Unity/Hub/Editor/2019.3.11f1/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-projectPath "$(pwd)/build/unity/$source/demo" \
	-exportPackage \
		"Assets/Plugins" \
		"Assets/SuperAwesome" \
		"$(pwd)/build/unity/$project.unitypackage" \
	-logFile -\
	-quit
