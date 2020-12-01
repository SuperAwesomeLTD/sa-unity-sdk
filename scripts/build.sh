#!/bin/bash -ex

# Set the release version number for AwesomeAds Unity SDK
unityReleaseSdkVersion="8.0.0-alpha1"

# Set the versions for dependencies
androidSdkVersion="8.0.0-alpha4"
iosSdkVersion="8.0"

# Set the Unity Installation version
unityVersion="2019.3.11f1"

start=`date +%s`

. ./download-unity.sh
. ./build-unity.sh
. ./build-admob-unity.sh
. ./build-base-unity.sh
. ./package-unity.sh

end=`date +%s`
runtime=$((end-start))

echo "Build completed for" $unityReleaseSdkVersion "in" $runtime "seconds"
