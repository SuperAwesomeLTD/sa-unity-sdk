#!/bin/bash -ex

project="SuperAwesomeSDK"
rm -rf build/unity/sa-unity-sdk

# clone the repo
source=sa-unity-sdk

cp -r source/unity/* build/unity/

mkdir build/unity/$source/demo/Assets/Plugins/
mkdir build/unity/$source/demo/Assets/Plugins/iOS
mkdir build/unity/$source/demo/Assets/Plugins/Android
mkdir build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib

# Add Dependency xml for the Universal-Jar-Resolver

dependencyFile=build/unity/$source/demo/Assets/SuperAwesome/Editor/"SuperAwesomeDependencies.xml"
echo "<dependencies>" > $dependencyFile
echo "  <androidPackages>" >> $dependencyFile
echo "    <repositories>" >> $dependencyFile
echo "      <repository>https://repo.maven.apache.org/maven2</repository>" >> $dependencyFile
echo "      <repository>http://dl.bintray.com/superawesome/SuperAwesomeSDK</repository>" >> $dependencyFile
echo "    </repositories>" >> $dependencyFile

echo "    <androidPackage spec=\"tv.superawesome.sdk.publisher:superawesome:$androidSdkVersion\" />" >> $dependencyFile
echo "  </androidPackages>" >> $dependencyFile

echo "  <iosPods>" >> $dependencyFile
echo "    <iosPod name=\"SuperAwesome\" version=\"~> $iosSdkVersion\" bitcodeEnabled=\"true\" minTargetSdk=\"10.0\" />" >> $dependencyFile
echo "  </iosPods>" >> $dependencyFile
echo "</dependencies>" >> $dependencyFile

# add android sources

projectProperties=build/unity/$source/demo/Assets/Plugins/Android/SuperAwesome_lib/"project.properties"
echo "# Project target." > $projectProperties
echo "target=android-11" >> $projectProperties
echo "android.library=true" >> $projectProperties

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
