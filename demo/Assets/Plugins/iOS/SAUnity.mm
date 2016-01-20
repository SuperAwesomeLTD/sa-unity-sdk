//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

extern "C" {
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and loads an Ad with the help of the SuperAwesome iOS SDK
    void SuperAwesomeUnityLoadAd(const char *unityName, int placementId, BOOL isTestingEnabled) {
        // transfrom the name
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        // create a linker
        SALoaderUnityLinker *linker = [[SALoaderUnityLinker alloc] init];
        [linker loadAd:placementId
            forUnityAd:name
          withTestMode:isTestingEnabled
            andSuccess:^(NSString *unityAd, NSString *adString) {
            // the Unity callback function
            UnitySendMessage([unityAd UTF8String],
                             "nativeCallback_LoadSuccess",
                             [adString UTF8String]);
        }
              andError:^(NSString *unityAd, NSInteger placementId) {
            // the Unity callback function
            UnitySendMessage([unityAd UTF8String],
                             "nativeCallback_LoadError",
                             [[NSString stringWithFormat:@"%ld", placementId] UTF8String]);
        }];
    }
    
    void SuperAwesomeUnityLoadAd2(const char *unityName, int placementId, BOOL isTestingEnabled) {
        // transfrom the name
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        // create a linker
        SALoaderUnityLinker *linker = [[SALoaderUnityLinker alloc] init];
        [linker loadAd:placementId
            forUnityAd:name
          withTestMode:isTestingEnabled
            andSuccess:^(NSString *unityAd, NSString *adString) {
                // the Unity callback function
                UnitySendMessage([unityAd UTF8String],
                                 "nativeCallback_LoadSuccess",
                                 [adString UTF8String]);
            }
              andError:^(NSString *unityAd, NSInteger placementId) {
                  // the Unity callback function
                  UnitySendMessage([unityAd UTF8String],
                                   "nativeCallback_LoadError",
                                   [[NSString stringWithFormat:@"%ld", placementId] UTF8String]);
              }];
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays an ad
    void SuperAwesomeUnityOpenVideoAd(const char *unityName, int placementId, const char *adJson, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAFullscreenVideoAdUnityLinker *linker = [[SAFullscreenVideoAdUnityLinker alloc] init];
        // start
        [linker startWithPlacementId:placementId
                           andAdJson:json
                        andUnityName:name
                  andHasParentalGate:isParentalGateEnabled
                   andHasCloseButton:shouldShowCloseButton
                      andClosesAtEnd:shouldAutomaticallyCloseAtEnd];
        
//        // parse sent data
//        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
//        NSString *name = [NSString stringWithUTF8String:adName];
//        BOOL isTest = testMode;
//        BOOL isParentalGate = gateEnabled;
//        
//        // start the linker
//        SAFullscreenVideoAdUnityLinker *linker = [[SAFullscreenVideoAdUnityLinker alloc] initWithVideoAd:[placementIDString intValue]
//                                                                                            andUnityName:name
//                                                                                                withGate:isParentalGate
//                                                                                              inTestMode:isTest
//                                                                                          hasCloseButton:true
//                                                                                          andClosesAtEnd:true];
//        [linker addLoadVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdLoaded", "");
//        }];
//        [linker addFailToLoadVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdFailedToLoad", "");
//        }];
//        [linker addStartVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdStartedPlaying", "");
//        }];
//        [linker addStopVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdStoppedPlaying", "");
//        }];
//        [linker addClickVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdClicked", "");
//        }];
//        [linker addFailToPlayVideoBlock:^(NSString *unityAd) {
//            UnitySendMessage([unityAd UTF8String], "videoAdFailedToPlay", "");
//        }];
//        
//        // start!!!!
//        [linker start];
    }
}