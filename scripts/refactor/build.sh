#!/bin/bash -ex

./1.download-android.sh
./2.download-ios.sh
./3.download-unity.sh
./4.build-android.sh
./5.build-ios.sh
./6.build-unity.sh
./7.build-admob-unity.sh
./8.build-base-unity.sh
./9.package-unity.sh
