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
        
        // assign the success and error callbacks
        linker.event = ^(NSString *unityAd, NSString *unityCallback, NSString *adString) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\", \"adJson\":%@}", unityCallback, adString];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // call to load
        [linker loadAd:placementId
            forUnityAd:name
          withTestMode:isTestingEnabled];
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays an ad
    void SuperAwesomeUnitySAVideoAd(const char *unityName, int placementId, const char *adJson, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAFullscreenVideoAdUnityLinker *linker = [[SAFullscreenVideoAdUnityLinker alloc] init];
        
        // add callbacks
        linker.event = ^(NSString *unityAd, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\"}", unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker startWithPlacementId:placementId
                           andAdJson:json
                        andUnityName:name
                  andHasParentalGate:isParentalGateEnabled
                   andHasCloseButton:shouldShowCloseButton
                      andClosesAtEnd:shouldAutomaticallyCloseAtEnd];
    }
    
    void SuperAwesomeUnitySAInterstitialAd(const char *unityName, int placementId, const char *adJson, BOOL isParentalGateEnabled) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAInterstitialAdUnityLinker *linker = [[SAInterstitialAdUnityLinker alloc] init];
        
        // add callbacks
        linker.event = ^(NSString *unityAd, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\"}", unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker startWithPlacementId:placementId
                           andAdJson:json
                        andUnityName:name
                  andHasParentalGate:isParentalGateEnabled];
    }
}