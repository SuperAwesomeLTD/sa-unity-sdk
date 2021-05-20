#!/bin/bash -ex

############################################################ 
# step 1: cleanup old build folder
############################################################

source=sa-unity-sdk

rm -rf build/unity/*
cp -r source/unity/ build/unity/

mkdir build/unity/$source/demo/Assets/Plugins/
mkdir build/unity/$source/demo/Assets/Plugins/iOS
mkdir build/unity/$source/demo/Assets/Plugins/Android
mkdir build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib

############################################################ 
# step 2: add android resources
############################################################

# make manifest & prop work for unity <2019.x versions
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

# make manifest work for unity 2020.x versions
mkdir build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome.androidlib/
cp -r build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib/* build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome.androidlib/

# copy jar files
cp -r build/android/*.jar build/unity/$source/demo/Assets/Plugins/Android/

############################################################ 
# step 3: add ios resources
############################################################

cp -r build/ios/* build/unity/$source/demo/Assets/Plugins/iOS/

############################################################ 
# step 4: build as UnityPackage
############################################################

/Applications/Unity/Hub/Editor/2019.3.10f1/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-projectPath "$(pwd)/build/unity/$source/demo" \
	-exportPackage \
		"Assets/Plugins" \
		"Assets/SuperAwesome" \
		"$(pwd)/build/unity/SuperAwesomeSDK.Unity.full.unitypackage" \
	-logFile -\
	-quit