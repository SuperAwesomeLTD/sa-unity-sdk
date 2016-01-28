To complete integrating the SDK for iOS, you'll need to follow the next steps (once):

#### Build the project for iOS

To do this, click on `File > Build Settings` menu.
There, select the `iOS` option and check the `Symlink Unity Libraries` and `Development build` options.
Then, click on `Build` and save the new XCode project on your drive.

![](img/IMG_04_iOSBuild.png "iOS Build")

#### Adding the SuperAwesome SDK via CocoaPods

Next, you'll need to add the AwesomeAds iOS SDK. You can find more information [here](https://developers.superawesome.tv/docs/iossdk?version=4) or you can follow the quick guide below:

Install CocoaPods (if you haven't already):

```
sudo gem install cocoapods

```

Go to your project's directory and initialise CocoaPods:

```
cd /project_root
pod init

```

This will create a new file simply called `Podfile`. Open it and alter it to look like this:

```
# Uncomment this line to define a global platform for your project
platform :ios, '6.0'

target 'Unity-iPhone' do
pod 'SuperAwesome'
end

target 'Unity-iPhone Tests' do

end

```

Then execute:

```
pod update

```

to tell CocoaPods to add the SuperAwesome iOS SDK library to your project. 
Don't forget to open the .xcworkspace file to open your project in Xcode, instead of the .xcproj file, from here on out.

#### Final setup

After the CocoaPod dependency has been added, you have to make some changes to the default Unity build configuration, as the CocoaPods settings need to be propagated in the build target but won't have done so since Unity has already set these values.

In the `Build Settings` tab you will need to search for each of `OTHER_LDFLAGS`, `OTHER_CFLAGS` and `HEADER_SEARCH_PATHS`, double-click on them, and add `$(inherited)` to the list of existing values for these settings. You likely will have also received a message when running `pod update`, warning you to do this.

![](img/IMG_05.png)
![](img/IMG_06.png)
![](img/IMG_07.png)

Finally, when targeting devices for iOS 9 onwards, don't forget to add, for the moment, the following key to your plist file:

```
<dict>
	<key>NSAllowsArbitraryLoads</key>
	<true/>
</dict>

```

to be able to load data over both HTTPS and HTTP.

Once this is done your iOS project will be ready to use and any calls to the native SDK from your Unity project will work as expected.
