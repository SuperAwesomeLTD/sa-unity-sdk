#!/bin/bash -ex

############################## 
# download android source code
##############################

rm -rf source/android/*

git clone -b  master https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android.git source/android/sa-mobile-sdk-android
git clone -b  master https://github.com/SuperAwesomeLTD/sa-mobile-lib-android-videoplayer.git source/android/sa-mobile-lib-android-videoplayer