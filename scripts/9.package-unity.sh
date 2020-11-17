#!/bin/bash -ex

publisherProject="SuperAwesomeSDK"
publisherVersion="7.2.17"

cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$publisherVersion.unitypackage

publisherProject="SuperAwesomeBaseSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$publisherVersion.unitypackage

publisherProject="SuperAwesomeAdMobSDK"
cp build/unity/$publisherProject.unitypackage outputs/$publisherProject-$publisherVersion.unitypackage

