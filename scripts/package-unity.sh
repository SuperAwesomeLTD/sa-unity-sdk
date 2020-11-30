#!/bin/bash -ex

publisherProject="SuperAwesomeSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$buildSdkVersion.unitypackage

publisherProject="SuperAwesomeBaseSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$buildSdkVersion.unitypackage

publisherProject="SuperAwesomeAdMobSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$buildSdkVersion.unitypackage

