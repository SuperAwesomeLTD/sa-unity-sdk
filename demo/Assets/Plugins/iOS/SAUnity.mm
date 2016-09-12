//
//  SAUnity.mm
//  Pods
//
//  Created by Gabriel Coman on 19/07/2016.
//
//

#import <Foundation/Foundation.h>

#if defined(__has_include)
#if __has_include("SuperAwesomeSDKUnity.h")
#import "SuperAwesomeSDKUnity.h"
#else 
#import "SuperAwesome.h"
#import "SAUnityLoadAd.h"
#import "SAUnityPlayBannerAd.h"
#import "SAUnityPlayInterstitialAd.h"
#import "SAUnityPlayFullscreenVideoAd.m"
#endif
#endif

void UnitySendMessage(const char *identifier, const char *function, const char *payload);

extern "C" {
    
//    NSMutableDictionary *extensionDict = [[NSMutableDictionary alloc] init];
    NSMutableDictionary *bannerDictionary = [@{} mutableCopy];
    
//    //
//    // Setter / getter and remover functions for linker dictionary objects
//    // @warn: this should be compatible with both Unity 4- and Unity 5+
//    SAUnityExtension *getOrCreateExtension(NSString *name) {
//        return [extensionDict objectForKey:name];
//    }
//    
//    void removeExtension(NSString *name){
//        if ([extensionDict objectForKey:name]){
//            [extensionDict removeObjectForKey:name];
//        }
//    }
    
    void sendToUnity (NSString *unityName, NSInteger placementId, NSString *callback) {
        
        // form payload
        NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"sacallback_%@\"}", placementId, callback];
        
        // send message to unity
        UnitySendMessage([unityName UTF8String], "nativeCallback", [payload UTF8String]);
    }
    
    void SuperAwesomeUnitySABannerAdCreate (const char *unityName) {
        
        
        // get the key
        __block NSString *key = [NSString stringWithUTF8String:unityName];
        
        // create a new banner
        SABannerAd *banner = [[SABannerAd alloc] init];
        
        // set banner callback
        [banner setCallback:^(NSInteger placementId, SAEvent event) {
            switch (event) {
                case adLoaded: sendToUnity(key, placementId, @"adLoaded"); break;
                case adFailedToLoad: sendToUnity(key, placementId, @"adFailedToLoad"); break;
                case adShown: sendToUnity(key, placementId, @"adShown"); break;
                case adFailedToShow: sendToUnity(key, placementId, @"adFailedToShow"); break;
                case adClicked: sendToUnity(key, placementId, @"adClicked"); break;
                case adClosed: sendToUnity(key, placementId, @"adClosed"); break;
            }
        }];
        
        // save the banner
        [bannerDictionary setObject:banner forKey:key];
    }
    
    void SuperAwesomeUnitySABannerAdLoad (const char *unityName, int placementId, int configuration, bool test) {
        
        // get the key
        NSString *key = [NSString stringWithUTF8String:unityName];
        
        if ([bannerDictionary objectForKey:key]) {
         
            // get banner
            SABannerAd *banner = [bannerDictionary objectForKey:key];
            
            // customize before loading
            [banner setConfiguration: configuration];
            [banner setTest: test];
            
            // load data
            [banner load:placementId];
        } else {
            // handle failure
        }
    }
    
    bool SuperAwesomeUnitySABannerAdHasAdAvailable (const char *unityName) {
        
        // get the key
        NSString *key = [NSString stringWithUTF8String:unityName];
        
        if ([bannerDictionary objectForKey:key]) {
            // get banner
            SABannerAd *banner = [bannerDictionary objectForKey:key];
            
            // return
            return [banner hasAdAvailable];
        }
        
        return false;
    }
    
    void SuperAwesomeUnitySABannerAdPlay (const char *unityName, bool isParentalGateEnabled, int position, int size, int color) {
        
        // get the key
        NSString *key = [NSString stringWithUTF8String:unityName];
        
        if ([bannerDictionary objectForKey:key]) {
            
            // get the root vc
            UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
            
            // calculate the size of the ad
            __block CGSize realSize = CGSizeZero;
            if (size == 1) realSize = CGSizeMake(300, 50);
            else if (size == 2) realSize = CGSizeMake(728, 90);
            else if (size == 3) realSize = CGSizeMake(300, 250);
            else realSize = CGSizeMake(320, 50);
            
            // get the screen size
            __block CGSize screen = [UIScreen mainScreen].bounds.size;
            
            if (realSize.width > screen.width) {
                realSize.height = (screen.width * realSize.height) / realSize.width;
                realSize.width = screen.width;
            }
            
            // calculate the position of the ad
            __block CGPoint realPos = position == 0 ?
            CGPointMake((screen.width - realSize.width) / 2.0f, 0) :
            CGPointMake((screen.width - realSize.width) / 2.0f, screen.height - realSize.height);
        
            // get banner
            SABannerAd *banner = [bannerDictionary objectForKey:key];
            
            // parental gate
            [banner setIsParentalGateEnabled:isParentalGateEnabled];
            
            // position
            banner.backgroundColor = color == 0 ? [UIColor clearColor] : [UIColor colorWithRed:191.0/255.0f green:191.0/255.0f blue:191.0/255.0f alpha:1];
            
            // get root vc, show fvad and then play it
            [root.view addSubview:banner];
            
            // set the banner's frame
            [banner resize:CGRectMake(realPos.x, realPos.y, realSize.width, realSize.height)];
            
            // add a block notification
            [[NSNotificationCenter defaultCenter] addObserverForName:@"UIDeviceOrientationDidChangeNotification"
                                                              object:nil
                                                               queue:nil
                                                          usingBlock:
             ^(NSNotification * note) {
                 screen = [UIScreen mainScreen].bounds.size;
                 
                 if (size == 1) realSize = CGSizeMake(300, 50);
                 else if (size == 2) realSize = CGSizeMake(728, 90);
                 else if (size == 3) realSize = CGSizeMake(300, 250);
                 else realSize = CGSizeMake(320, 50);
                 
                 if (realSize.width > screen.width) {
                     realSize.height = (screen.width * realSize.height) / realSize.width;
                     realSize.width = screen.width;
                 }
                 
                 if (position == 0) realPos = CGPointMake((screen.width - realSize.width) / 2.0f, 0);
                 else realPos = CGPointMake((screen.width - realSize.width) / 2.0f, screen.height - realSize.height);
                 
                 [banner resize:CGRectMake(realPos.x, realPos.y, realSize.width, realSize.height)];
             }];

            
            // finally play
            [banner play];
            
        } else {
            // handle failure
        }
    }
    
    void SuperAwesomeUnitySABannerAdClose (const char *unityName) {
        
        // get the key
        NSString *key = [NSString stringWithUTF8String:unityName];
        
        if ([bannerDictionary objectForKey:key]) {
            
            // get banner
            SABannerAd *banner = [bannerDictionary objectForKey:key];
            
            // close
            [banner close];
            
            // remove
            [banner removeFromSuperview];
            
            // remove from dictionary
            [bannerDictionary removeObjectForKey:key];
        } else {
            // handle failure
        }
    }
    
    
    void SuperAwesomeUnitySAInterstitialAdCreate () {

        // set callback
        [SAInterstitialAd setCallback:^(NSInteger placementId, SAEvent event) {
            switch (event) {
                case adLoaded: sendToUnity(@"SAInterstitialAd", placementId, @"adLoaded"); break;
                case adFailedToLoad: sendToUnity(@"SAInterstitialAd", placementId, @"adFailedToLoad"); break;
                case adShown: sendToUnity(@"SAInterstitialAd", placementId, @"adShown"); break;
                case adFailedToShow: sendToUnity(@"SAInterstitialAd", placementId, @"adFailedToShow"); break;
                case adClicked: sendToUnity(@"SAInterstitialAd", placementId, @"adClicked"); break;
                case adClosed: sendToUnity(@"SAInterstitialAd", placementId, @"adClosed"); break;
            }
        }];

    }
    
    void SuperAwesomeUnitySAInterstitialAdLoad (int placementId, int configuration, bool test) {
        
        // set configuration
        [SAInterstitialAd setConfiguration:configuration];
        
        // set test
        [SAInterstitialAd setTest:test];
        
        // load
        [SAInterstitialAd load: placementId];
    }
    
    bool SuperAwesomeUnitySAInterstitialAdHasAdAvailable() {
        return [SAVideoAd hasAdAvailable];
    }
    
    void SuperAwesomeUnitySAInterstitialAdPlay (bool isParentalGateEnabled, bool shouldLockOrientation, int lockOrientation) {
        
        // configure pg
        [SAInterstitialAd setIsParentalGateEnabled: isParentalGateEnabled];
         
        // configure
        [SAInterstitialAd setShouldLockOrientation: shouldLockOrientation];
        
        // configure orientation
        NSUInteger orientation = UIInterfaceOrientationMaskAll;
        switch (lockOrientation) {
            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
        }
        
        [SAInterstitialAd setLockOrientation:orientation];
        
        // play
        UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
        [SAInterstitialAd play: root];
        
    }
    
    void SuperAwesomeUnitySAVideoAdCreate () {
        
        // set callback
        [SAVideoAd setCallback:^(NSInteger placementId, SAEvent event) {
            switch (event) {
                case adLoaded: sendToUnity(@"SAVideoAd", placementId, @"adLoaded"); break;
                case adFailedToLoad: sendToUnity(@"SAVideoAd", placementId, @"adFailedToLoad"); break;
                case adShown: sendToUnity(@"SAVideoAd", placementId, @"adShown"); break;
                case adFailedToShow: sendToUnity(@"SAVideoAd", placementId, @"adFailedToShow"); break;
                case adClicked: sendToUnity(@"SAVideoAd", placementId, @"adClicked"); break;
                case adClosed: sendToUnity(@"SAVideoAd", placementId, @"adClosed"); break;
            }
        }];
    }
    
    void SuperAwesomeUnitySAVideoAdLoad(int placementId, int configuration, bool test) {
        
        // set configuration
        [SAVideoAd setConfiguration:configuration];
        
        // set test
        [SAVideoAd setTest:test];
        
        // load
        [SAVideoAd load: placementId];
        
    }
    
    bool SuperAwesomeUnitySAVideoAdHasAdAvailable() {
        return [SAVideoAd hasAdAvailable];
    }
    
    void SuperAwesomeUnitySAVideoAdPlay(bool isParentalGateEnabled, bool shouldShowCloseButton, bool shouldShowSmallClickButton, bool shouldAutomaticallyCloseAtEnd, bool shouldLockOrientation, int lockOrientation) {
        
        // configure pg
        [SAVideoAd setIsParentalGateEnabled: isParentalGateEnabled];
        
        // lock orientation
        [SAVideoAd setShouldLockOrientation: shouldLockOrientation];
        
        // configure orientation
        NSUInteger orientation = UIInterfaceOrientationMaskAll;
        switch (lockOrientation) {
            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
        }
        
        [SAVideoAd setLockOrientation:orientation];
        
        // close button
        [SAVideoAd setShouldShowCloseButton: shouldShowCloseButton];
        
        // small click button
        [SAVideoAd setShouldShowSmallClickButton: shouldShowSmallClickButton];
        
        // auto-close
        [SAVideoAd setShouldAutomaticallyCloseAtEnd: shouldAutomaticallyCloseAtEnd];
        
        // play
        UIViewController *root = [UIApplication sharedApplication].keyWindow.rootViewController;
        [SAVideoAd play: root];
        
    }
    
//    //
//    // This function acts as a bridge between Unity-iOS-Unity
//    // and loads an Ad with the help of the SuperAwesome iOS SDK
//    void SuperAwesomeUnityLoadAd(const char *unityName, int placementId, BOOL isTestingEnabled, int config) {
//        // transfrom the name
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSLog(@"SuperAwesomeUnityLoadAd - %@", name);
//        
//        // create a linker
//        SAUnityLoadAd *extension = (SAUnityLoadAd*)getOrCreateExtension(name);
//        if (!extension) {
//            extension = [[SAUnityLoadAd alloc] init];
//            [extensionDict setObject:extension forKey: name];
//        }
//        
//        // assign the success and error callbacks
//        extension.loadingEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback, NSString *adString) {
//            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\", \"adJson\":%@}", placementId, unityCallback, adString];
//            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
//        };
//        
//        // call to load
//        [extension loadAd:placementId
//               forUnityAd:name
//             withTestMode:isTestingEnabled
//        withConfiguration:config];
//    }
//    
//    //
//    // This function acts as a bridge between Unity-iOS-Unity
//    // and displays a banner ad
//    void SuperAwesomeUnitySABannerAd(int placementId, const char *adJson, const char *unityName, int position, int size, int color, BOOL isParentalGateEnabled) {
//        
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSString *json = [NSString stringWithUTF8String:adJson];
//        NSLog(@"SuperAwesomeUnitySABannerAd - %@", name);
//        
//        // updat-eeeeed!
//        SAUnityPlayBannerAd *extension = (SAUnityPlayBannerAd*)getOrCreateExtension(name);
//        if (!extension) {
//            extension = [[SAUnityPlayBannerAd alloc] init];
//            [extensionDict setObject:extension forKey: name];
//        }
//        
//        // add callbacks
//        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
//            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
//            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
//        };
//        
//        // start
//        [extension showBannerAdWith:placementId
//                          andAdJson:json
//                       andUnityName:name
//                        andPosition:position
//                            andSize:size
//                           andColor:color
//                 andHasParentalGate:isParentalGateEnabled];
//    }
//    
//    //
//    // function that removes a banner ad
//    void SuperAwesomeUnityRemoveSABannerAd(const char *unityName) {
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSLog(@"SuperAwesomeUnityRemoveSABannerAd - %@", name);
//        
//        // updat-eeeeed!
//        SAUnityPlayBannerAd *extension = (SAUnityPlayBannerAd*)getOrCreateExtension(name);
//        if (extension) {
//            [extension removeBannerForUnityName:name];
//            removeExtension(name);
//        }
//    }
//    
//    //
//    // This function acts as a bridge between Unity-iOS-Unity
//    // and displays an interstitial ad
//    void SuperAwesomeUnitySAInterstitialAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldLockOrientation, int lockOrientation) {
//        
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSString *json = [NSString stringWithUTF8String:adJson];
//        NSLog(@"SuperAwesomeUnitySAInterstitialAd - %@", name);
//        
//        NSUInteger orientation = UIInterfaceOrientationMaskAll;
//        switch (lockOrientation) {
//            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
//            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
//        }
//
//        // updat-eeeeed!
//        SAUnityPlayInterstitialAd *extension = (SAUnityPlayInterstitialAd*)getOrCreateExtension(name);
//        if (!extension) {
//            extension = [[SAUnityPlayInterstitialAd alloc] init];
//            [extensionDict setObject:extension forKey: name];
//        }
//        
//        // add callbacks
//        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
//            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
//            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
//        };
//        
//        // start
//        [extension showInterstitialAdWith:placementId
//                                andAdJson:json
//                             andUnityName:name
//                       andHasParentalGate:isParentalGateEnabled
//                            andShouldLock:shouldLockOrientation
//                          lockOrientation:orientation];
//    }
//    
//    //
//    // function that removes an interstitial ad
//    void SuperAwesomeUnityCloseSAInterstitialAd(const char *unityName) {
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSLog(@"SuperAwesomeUnityCloseSAInterstitialAd - %@", name);
//        
//        // updat-eeeeed!
//        SAUnityPlayInterstitialAd *extension = (SAUnityPlayInterstitialAd*)getOrCreateExtension(name);
//        if (extension) {
//            [extension closeInterstitialForUnityName:name];
//            removeExtension(name);
//        }
//    }
//    
//    //
//    // This function acts as a bridge between Unity-iOS-Unity
//    // and displays a video ad
//    void SuperAwesomeUnitySAVideoAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd, BOOL shouldShowSmallClickButton, BOOL shouldLockOrientation, int lockOrientation) {
//        
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSString *json = [NSString stringWithUTF8String:adJson];
//        NSLog(@"SuperAwesomeUnitySAVideoAd - %@", name);
//        
//        NSUInteger orientation = UIInterfaceOrientationMaskAll;
//        switch (lockOrientation) {
//            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
//            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
//        }
//        
//        // updat-eeeeed!
//        SAUnityPlayFullscreenVideoAd *extension = (SAUnityPlayFullscreenVideoAd*)getOrCreateExtension(name);
//        if (!extension) {
//            extension = [[SAUnityPlayFullscreenVideoAd alloc] init];
//            [extensionDict setObject:extension forKey: name];
//        }
//        
//        // add callbacks
//        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
//            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
//            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
//        };
//        
//        // start
//        [extension showVideoAdWith:placementId
//                         andAdJson:json
//                      andUnityName:name
//                andHasParentalGate:isParentalGateEnabled
//                 andHasCloseButton:shouldShowCloseButton
//                    andClosesAtEnd:shouldAutomaticallyCloseAtEnd
//           andShouldShowSmallClick:shouldShowSmallClickButton
//                     andShouldLock:shouldLockOrientation
//                   lockOrientation:orientation];
//    }
//    
//    //
//    // function that closes a fullscreen ad
//    void SuperAwesomeUnityCloseSAFullscreenVideoAd(const char *unityName) {
//        // parse parameters
//        NSString *name = [NSString stringWithUTF8String:unityName];
//        NSLog(@"SuperAwesomeUnityCloseSAFullscreenVideoAd - %@", name);
//        
//        // updat-eeeeed!
//        SAUnityPlayFullscreenVideoAd *extension = (SAUnityPlayFullscreenVideoAd*)getOrCreateExtension(name);
//        if (extension) {
//            [extension closeFullscreenVideoForUnityName:name];
//            removeExtension(name);
//        }
//    }
}
