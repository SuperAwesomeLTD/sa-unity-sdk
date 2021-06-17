#!/bin/bash -ex

############################################################ 
# step 1: cleanup old build folder
############################################################

rm -rf build/ios/* || echo 'Already cleared'
mkdir build/ios/ || echo 'Already exists'

############################################################ 
# step 2: copy .mm source files & Moat library from project
############################################################

cp -r source/ios/sa-mobile-sdk-ios/Pod/Libraries/SUPMoatMobileAppKit.framework build/ios/
cp -r source/ios/sa-mobile-sdk-ios/Pod/Plugin/Unity/ build/ios/

############################################################ 
# step 3: cleanup some files that will make the build fail
############################################################

rm -rf source/ios/sa-mobile-sdk-ios/Pod/Plugin/Moat2/*

############################################################ 
# step 4: execute the builds for various architectures
############################################################

cd source/ios/sa-mobile-sdk-ios/Example
# pod update

# # build for device
# xcodebuild \
#   -workspace SuperAwesomeExample.xcworkspace \
#   -scheme 'SuperAwesome' \
#   -configuration 'Release' \
#   -destination 'generic/platform=iOS' \
#   -archivePath "../build/ios/SuperAwesome-iphoneos.xcarchive" \
#   SKIP_INSTALL=NO \
#   BUILD_LIBRARY_FOR_DISTRIBUTION=YES \
#   CONFIGURATION_BUILD_DIR='../build/ios/' \
#   archive \
#   || echo 'Build for iOS pseudo-failed. Moving on.'

# # build for device
# xcodebuild \
#   -workspace SuperAwesomeExample.xcworkspace \
#   -scheme 'SuperAwesome' \
#   -configuration 'Release' \
#   -destination 'generic/platform=iOS Simulator' \
#   -archivePath "build/ios/SuperAwesome-iphonesimulator.xcarchive" \
#   SKIP_INSTALL=NO \
#   BUILD_LIBRARY_FOR_DISTRIBUTION=YES \
#   CONFIGURATION_BUILD_DIR='../build/ios-simulator/' \
#   archive \
#   || echo 'Build for iOS pseudo-failed. Moving on.'

cd ../../../..

./2.3.prepare.all.ios.xcframework.sh -n "Moya"
./2.3.prepare.all.ios.xcframework.sh -n "Alamofire"
./2.3.prepare.all.ios.xcframework.sh -n "SwiftyXMLParser"
./2.3.prepare.all.ios.xcframework.sh -n "SuperAwesome"

# ############################################################ 
# step 6: ulterior change to SuperAwesome lib
# ############################################################

cd source/ios/sa-mobile-sdk-ios/Example/

# add a webkit import to the -Swift.h header file 
cd build/SuperAwesome.xcframework/ios-arm64_armv7/SuperAwesome.framework/Headers
echo '#import <AVFoundation/AVFoundation.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
echo '#import <WebKit/WebKit.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
cd ../../../../..

cd build/SuperAwesome.xcframework/ios-i386_x86_64-simulator/SuperAwesome.framework/Headers
echo '#import <AVFoundation/AVFoundation.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
echo '#import <WebKit/WebKit.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
cd ../../../../..