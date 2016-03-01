Once you have the MoPub unity plugin and AwesomeAds MoPub adapters into your Unity project, and have decided which types of ads to display, it's now time to export to iOS.

![](img/IMG_15_MoPub9.png "export to iOS")

### Getting the iOS SDK

The iOS project you create will have most things ready, but will miss the actual AwesomeAds iOS SDK, that makes everything work.
To install it, you can either follow the instructions  [here](https://developers.superawesome.tv/docs/androidsdk/Getting%20Started/Adding%20the%20Library%20to%20Your%20Project?version=3) or follow the shortened version presented below:

First, you'll need to open up a Terminal window on your Mac, and install CocoaPods, a library dependency management tool.

```
sudo gem install cocoapods

```

Then go to the root folder of the project you just exported and init CocoaPods.

```
cd /unity_export_project_root
pod init

```

This will create a file called `Podfile`, which you'll need to edit to look like this:

```
# Uncomment this line to define a global platform for your project
platform :ios, '6.0'

target 'Unity-iPhone' do
pod 'SuperAwesome'
end

target 'Unity-iPhone Tests' do

end

```

Save and exit. Finally, in your Terminal window:

```
pod update

```

This will install the AwesomeAds SDK as a dependency of the Unity iOS project you just exported.
You'll be prompted by CocoaPods to use the **.xcworkspace** file from now one, not the **.xcproj** one.
