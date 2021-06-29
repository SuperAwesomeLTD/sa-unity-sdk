#!/bin/bash -ex

rm -rf build
rm -rf source
rm -rf output

while getopts "p:t:b:" arg
do
  case "${arg}" in
    t) tag=${OPTARG};;
  esac
  case "${arg}" in
    p) platform=${OPTARG};;
  esac
  case "${arg}" in
    b) branch=${OPTARG};;
  esac
done

# TODO implement tag and make it override branch


############################################################
# step 1: download android source code
############################################################

if [[ $platform == 'android' || $platform == 'all' ]]
then
  # rm -rf source/android/*
  git clone -b $branch https://github.com/SuperAwesomeLTD/sa-mobile-sdk-android.git source/android/sa-mobile-sdk-android
  git clone -b master https://github.com/SuperAwesomeLTD/sa-mobile-lib-android-videoplayer.git source/android/sa-mobile-lib-android-videoplayer
fi

############################################################
# step 2: download iOS source code
############################################################

if [[ $platform == 'ios' || $platform == 'all' ]]
then
  # rm -rf source/ios/*
  git clone -b $branch https://github.com/SuperAwesomeLTD/sa-mobile-sdk-ios.git source/ios/sa-mobile-sdk-ios
fi
############################################################
# step 3: download Unity source code
############################################################

# TODO this shouldn't be pulled down from github but copied locally
# rm -rf source/unity/*
git clone -b master https://github.com/SuperAwesomeLTD/sa-unity-sdk.git source/unity/sa-unity-sdk

mkdir build
mkdir output
