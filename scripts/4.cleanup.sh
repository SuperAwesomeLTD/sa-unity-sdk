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
mkdir ../releases/ || echo "folder already exists"

# version
if [[ $version -eq '' ]]
then
  echo 'No version specified, leaving as is'.
else 
  mv output/SuperAwesomeSDK.Unity.full.unitypackage ../releases/SuperAwesomeSDK-$version.Unity.full.unitypackage
  rm -rf output
fi