#!/bin/bash -ex

rm -rf build

############################################################ 
# step 1: download android source code
############################################################

rm -rf source/android/*

git clone -b  master https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android.git source/android/sa-mobile-sdk-android
git clone -b  master https://github.com/SuperAwesomeLTD/sa-mobile-lib-android-videoplayer.git source/android/sa-mobile-lib-android-videoplayer

############################################################ 
# step 2: download iOS source code
############################################################

rm -rf source/ios/*

git clone -b master https://github.com/SuperAwesomeLTD/sa-mobile-sdk-ios.git source/ios/sa-mobile-sdk-ios

############################################################ 
# step 3: download Unity source code
############################################################

rm -rf source/unity/*

git clone -b master https://github.com/SuperAwesomeLTD/sa-unity-sdk.git source/unity/sa-unity-sdk

mkdir build