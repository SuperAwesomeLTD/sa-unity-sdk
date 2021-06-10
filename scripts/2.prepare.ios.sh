#!/bin/bash -ex

############################################################ 
# step 1: cleanup old build folder
############################################################

rm -rf build/ios/* || echo 'Already cleared'
mkdir build/ios/

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

# remove the arm64 architecture from the simulator build
# it's weird that it's there to begin with, but oh well
cd build/ios-simulator/SuperAwesome.framework/
lipo -remove arm64 SuperAwesome -o SuperAwesome || echo 'Arch already removed'
cd ../../..

############################################################ 
# step 5: use lipo to join the sim & phone libs into one binary
############################################################

lipo -create build/ios/SuperAwesome.framework/SuperAwesome build/ios-simulator/SuperAwesome.framework/SuperAwesome -output build/SuperAwesome

############################################################ 
# step 6: merge files from the sim & phone libs into one fat one and do some final prep
############################################################

# merge the sim & phone libraries
rm -rf build/fat 
mkdir build/fat
mkdir build/fat/SuperAwesome.framework
cp -r build/ios-simulator/SuperAwesome.framework/ build/fat/SuperAwesome.framework/
cp build/SuperAwesome build/fat/SuperAwesome.framework/SuperAwesome
rm -rf build/ios/SuperAwesome.framework/Modules/SuperAwesome.swiftmodule/Project
cp -r build/ios/SuperAwesome.framework/Modules/SuperAwesome.swiftmodule/* build/fat/SuperAwesome.framework/Modules/SuperAwesome.swiftmodule/

# add a webkit import to the -Swift.h header file 
cd build/fat/SuperAwesome.framework/Headers
echo -e "#import <WebKit/WebKit.h>\n$(cat SuperAwesome-Swift.h)" > SuperAwesome-Swift.h
cd ../../../..

############################################################ 
# step 7: finally, copy the new fat framework into the build folder
############################################################

cd ../../../..
cp -r source/ios/sa-mobile-sdk-ios/Example/build/fat/SuperAwesome.framework build/ios/