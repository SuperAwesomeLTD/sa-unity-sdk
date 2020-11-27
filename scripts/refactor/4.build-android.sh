#!/bin/bash -ex

rm -rf build/android/*

sources=(
	"sa-mobile-sdk-android"
    "sa-mobile-lib-android-videoplayer"
)

outputs=(
	"superawesome-base"
	"saunity"
	"savideoplayer"
	"saadmob"
)

libraries=(
	"moatlib.jar"
	"unity-classes.jar"
)

for source in ${sources[*]}
do
	# prepare info
	sourceFolder=source/android/$source 		# /source/android/sa-mobile-lib-android-vastparser

	# write local properties
	localProperties="$sourceFolder/local.properties"
	echo "sdk.dir=/Users/gunhan.sancar/Library/Android/sdk" >> $localProperties

	# build the actual thing
	cd $sourceFolder
	./gradlew assemble
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
