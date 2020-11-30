#!/bin/bash -ex

buildSdkVersion="8.0.0-alpha1"
androidSdkVersion="8.0.0-alpha3"
iosSdkVersion="8.0"
unityVersion="2019.3.11f1"

# . ./download-unity.sh
. ./build-unity.sh
. ./build-admob-unity.sh
. ./build-base-unity.sh
. ./package-unity.sh
