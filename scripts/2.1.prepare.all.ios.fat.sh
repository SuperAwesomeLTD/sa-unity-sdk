# run this from build/ folder

# get name of the framework
while getopts n: flag
do 
  case "${flag}" in
    n) name=${OPTARG};;
  esac
done

path=source/ios/sa-mobile-sdk-ios/Example

# cleanup pre-existing
rm -rf $path/build/fat/$name.framework
# re-create framework folder
mkdir $path/build/fat/$name.framework
# remove redundant arch
# cd build/ios-simulator/SuperAwesome.framework/
lipo -remove arm64 $path/build/ios-simulator/$name.framework/$name -o $path/build/ios-simulator/$name.framework/$name || echo 'Arch already removed'
# copy all files
cp -r $path/build/ios-simulator/$name.framework/ $path/build/fat/$name.framework/
# form the joined binary
lipo -create $path/build/ios/$name.framework/$name $path/build/ios-simulator/$name.framework/$name -output $path/build/fat/$name.framework/$name
# copy swiftmodule file
cp -r $path/build/ios/$name.framework/Modules/$name.swiftmodule/* $path/build/fat/$name.framework/Modules/$name.swiftmodule/