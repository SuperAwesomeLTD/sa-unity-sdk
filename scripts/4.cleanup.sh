#!/bin/bash -ex

# cleanup intermediary files
rm -rf build
rm -rf source

# get version flag
while getopts v: flag
do 
  case "${flag}" in
    v) version=${OPTARG};;
  esac
done

# give final name to output
if [[ $version -eq '' ]]
then
  echo 'No version specified, leaving as is'.
else 
  mv output/SuperAwesomeSDK.Unity.full.unitypackage output/SuperAwesomeSDK-$version.Unity.full.unitypackage
fi