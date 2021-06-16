#!/bin/bash -ex

############################################################ 
# step 1: cleanup old build folder
############################################################

rm -rf build/android/*
mkdir build/android

############################################################ 
# step 2: setup liraries, outputs, sources, etc for Android
############################################################

sources=("sa-mobile-sdk-android" "sa-mobile-lib-android-videoplayer")
outputs=("superawesome-base" "saunity" "savideoplayer")
libraries=("moatlib.jar" "unity-classes.jar")

############################################################ 
# step 3: actually build everything
############################################################

for source in ${sources[*]}
do
	# prepare info
	sourceFolder=source/android/$source 		# /source/android/sa-mobile-lib-android-vastparser

	# write local properties
	localProperties="$sourceFolder/local.properties"
	echo "sdk.dir=$ANDROID_SDK_ROOT" >> $localProperties

	# build the actual thing
	cd $sourceFolder
	./gradlew build
	cd ../../..

	# copy all outputs over
	for output in ${outputs[*]}
	do
		# /source/android/sa-mobile-lib-android-vastparser/savastparser/build/outputs/aar
		outputFolder=$sourceFolder/$output/build/outputs/aar
		# savastparser-release.aar
		outputName=$output-release.aar

		if [ -f $outputFolder/$outputName ]
		then
			cp $outputFolder/$outputName build/android/$output.zip
			unzip build/android/$output.zip -d build/android/$output
			rm build/android/$output.zip
			cp build/android/$output/classes.jar build/android/$output.jar
			rm -rf build/android/$output
		fi

		# copy dependant libraries
		for library in ${libraries[*]}
		do
			libraryFolder=$sourceFolder/$output/libs

			if [ -f $libraryFolder/$library ]
			then
				cp $libraryFolder/$library build/android/$library
			fi
		done
	done
done
