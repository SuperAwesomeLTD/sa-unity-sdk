#!/bin/bash -ex

publisherProject="SuperAwesomeSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$unityReleaseSdkVersion.unitypackage

publisherProject="SuperAwesomeBaseSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$unityReleaseSdkVersion.unitypackage

publisherProject="SuperAwesomeAdMobSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$unityReleaseSdkVersion.unitypackage

