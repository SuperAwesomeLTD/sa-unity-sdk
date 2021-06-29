#!/bin/bash -ex

############################################################
# step 0: get flags
############################################################
while getopts "v:p:t:b:" arg
do
  case "${arg}" in
    v) version=${OPTARG};;
  esac
  case "${arg}" in
    t) tag=${OPTARG};;
  esac
  case "${arg}" in
    p) platform=${OPTARG};;
  esac
  case "${arg}" in
    b) branch=${OPTARG};;
  esac
done

if [[ $branch == '' ]]
then
  branch='master'
fi

if [[ $platform == '' || ($platform != 'android' && $platform != 'ios' && $platform != 'all') ]]
then
  platform='all'
fi

############################################################
# step 1: download the iOS, Android & Unity source code
############################################################

./0.download.sources.sh -b $branch -p $platform

############################################################
# step 2: Build Android & move result to correct build folder
############################################################

if [[ $platform == 'android' || $platform == 'all' ]]
then
  ./1.prepare.android.sh
fi
############################################################
# step 3: Build iOS & move result to correct build folder
############################################################

if [[ $platform == 'ios' || $platform == 'all' ]]
then
  ./2.prepare.ios.sh
fi
############################################################
# step 4: Build Unity & move result to correct build folder
############################################################

./3.build.unity.sh -p $platform

############################################################
# step 4: cleanup folders
############################################################

./4.cleanup.sh -v $version
