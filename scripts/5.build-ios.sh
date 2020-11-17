#!/bin/bash -ex

# Build Full version
cd ios-pod
pod repo remove trunk
pod update
pod install
cd ..

# Build AdMob Adapter
cd ios-admob-pod
pod repo remove trunk
pod update
pod install
cd ..

# Build Base Adapter
cd ios-base-pod
pod repo remove trunk
pod update
pod install
cd ..