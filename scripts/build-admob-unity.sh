#!/bin/bash -ex

project="SuperAwesomeAdMobSDK"
rm -rf build/unity/sa-unity-sdk

# clone the repo
source=sa-unity-sdk

cp -r source/unity/* build/unity/

mkdir build/unity/$source/demo/Assets/Plugins/
mkdir build/unity/$source/demo/Assets/Plugins/iOS
mkdir build/unity/$source/demo/Assets/Plugins/Android

# Add Dependency xml for the Universal-Jar-Resolver

dependencyFile=build/unity/$source/demo/Assets/SuperAwesome/Editor/"SuperAwesomeDependencies.xml"
echo "<dependencies>" > $dependencyFile
echo "  <androidPackages>" >> $dependencyFile
echo "    <repositories>" >> $dependencyFile
echo "      <repository>https://repo.maven.apache.org/maven2</repository>" >> $dependencyFile
echo "      <repository>http://dl.bintray.com/superawesome/SuperAwesomeSDK</repository>" >> $dependencyFile
echo "    </repositories>" >> $dependencyFile

echo "    <androidPackage spec=\"tv.superawesome.sdk.publisher:superawesome:$androidSdkVersion\" />" >> $dependencyFile
echo "    <androidPackage spec=\"tv.superawesome.sdk.publisher:superawesome-admob:$androidSdkVersion\" />" >> $dependencyFile
echo "    <androidPackage spec=\"tv.superawesome.sdk.publisher:superawesome-unity:$androidSdkVersion\" />" >> $dependencyFile
echo "  </androidPackages>" >> $dependencyFile

echo "  <iosPods>" >> $dependencyFile
echo "    <iosPod name=\"SuperAwesomeAdMob\" version=\"~> $iosSdkVersion\" bitcodeEnabled=\"true\" minTargetSdk=\"10.0\" />" >> $dependencyFile
echo "  </iosPods>" >> $dependencyFile
echo "</dependencies>" >> $dependencyFile

# build as UnityPackage
/Applications/Unity/Hub/Editor/$unityVersion/Unity.app/Contents/MacOS/Unity \
	-gvh_disable \
	-batchmode \
	-projectPath "$(pwd)/build/unity/$source/demo" \
	-exportPackage \
		"Assets/Plugins" \
		"Assets/SuperAwesome" \
		"$(pwd)/build/unity/$project.unitypackage" \
	-logFile -\
	-quit
