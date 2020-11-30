#!/bin/bash -ex

project="SuperAwesomeSDK"

rm -rf source/unity/
mkdir source/unity
mkdir source/unity/sa-unity-sdk

# clone the repo
source=sa-unity-sdk
repository=https://github.com/SuperAwesomeLTD/$source.git
destination=source/unity/$source
git clone -b refactor $repository $destination
