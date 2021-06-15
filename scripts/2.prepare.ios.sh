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
pod update

# clean project
xcodebuild -workspace SuperAwesomeExample.xcworkspace -scheme 'SuperAwesome' clean

# build for device
xcodebuild \
  -workspace SuperAwesomeExample.xcworkspace \
  -scheme 'SuperAwesome' \
  -configuration 'Release' \
  -destination 'generic/platform=iOS' \
  SKIP_INSTALL=NO \
  BUILD_LIBRARY_FOR_DISTRIBUTION=YES \
  CONFIGURATION_BUILD_DIR='../build/ios/' \
  build \
  || echo 'Build for iOS pseudo-failed. Moving on.'

# clean project again
xcodebuild -workspace SuperAwesomeExample.xcworkspace -scheme 'SuperAwesome' clean

# build for simulator
xcodebuild \
  -workspace SuperAwesomeExample.xcworkspace \
  -scheme 'SuperAwesome' \
  -configuration 'Release' \
  -destination 'generic/platform=iOS Simulator' \
  SKIP_INSTALL=NO \
  CONFIGURATION_BUILD_DIR='../build/ios-simulator/' \
  build \
  || echo 'Build for iOS Simulator pseudo-failed. Moving on.'

# ############################################################ 
# step 5: generate fat frameworks
# ############################################################

rm -rf build/fat || "already deleted" 
mkdir build/fat || "already created"
cd ../../../..
pwd

./2.1.prepare.all.ios.fat.sh -n "Moya"
./2.1.prepare.all.ios.fat.sh -n "Alamofire"
./2.1.prepare.all.ios.fat.sh -n "SwiftyXMLParser"
./2.1.prepare.all.ios.fat.sh -n "SuperAwesome"

# ############################################################ 
# step 6: ulterior change to SuperAwesome lib
# ############################################################

cd source/ios/sa-mobile-sdk-ios/Example/

# add a webkit import to the -Swift.h header file 
cd build/fat/SuperAwesome.framework/Headers
echo '#import <AVFoundation/AVFoundation.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
echo '#import <WebKit/WebKit.h>' | cat - SuperAwesome-Swift.h > temp && mv temp SuperAwesome-Swift.h
cd ../../../..

# ############################################################ 
# # step 7: finally, copy the new fat framework into the build folder
# ############################################################

cd ../../../..
cp -r source/ios/sa-mobile-sdk-ios/Example/build/fat/SuperAwesome.framework build/ios/
cp -r source/ios/sa-mobile-sdk-ios/Example/build/fat/Moya.framework build/ios/
cp -r source/ios/sa-mobile-sdk-ios/Example/build/fat/Alamofire.framework build/ios/
cp -r source/ios/sa-mobile-sdk-ios/Example/build/fat/SwiftyXMLParser.framework build/ios/