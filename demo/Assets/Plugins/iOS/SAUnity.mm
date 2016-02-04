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
    void SuperAwesomeUnityLoadAd(const char *unityName, int placementId, BOOL isTestingEnabled, int config) {
        // transfrom the name
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        SAConfiguration iconfig = (SAConfiguration)config;
        switch (config) {
            case PRODUCTION: [[SuperAwesome getInstance] setConfigurationProduction]; break;
            case STAGING: [[SuperAwesome getInstance] setConfigurationStaging]; break;
            case DEVELOPMENT: [[SuperAwesome getInstance] setConfigurationDevelopment]; break;
            default: break;
        }
        
        // create a linker
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnityLoadAd - %@", name);
        
        // assign the success and error callbacks
        linker.loadingEvent = ^(NSString *unityAd, NSString *unityCallback, NSString *adString) {
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
    // and displays a banner ad
    void SuperAwesomeUnitySABannerAd(int placementId, const char *adJson, const char *unityName, int position, int size, BOOL isParentalGateEnabled) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnitySABannerAd - %@", name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\"}", unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker showBannerAdWith:placementId
                       andAdJson:json
                    andUnityName:name
                     andPosition:position
                         andSize:size
              andHasParentalGate:isParentalGateEnabled];
    }
    
    //
    // function that removes a banner ad
    void SuperAwesomeUnityRemoveSABannerAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnityRemoveSABannerAd - %@", name);
        
        
        [linker removeBannerForUnityName:name];
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays an interstitial ad
    void SuperAwesomeUnitySAInterstitialAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled) {
        
        // NSLog(@"Got in SuperAwesomeUnitySAInterstitialAd");
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnitySAInterstitialAd - %@", name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\"}", unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker showInterstitialAdWith:placementId
                             andAdJson:json
                          andUnityName:name
                    andHasParentalGate:isParentalGateEnabled];
    }
    
    //
    // function that removes an interstitial ad
    void SuperAwesomeUnityCloseSAInterstitialAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnityCloseSAInterstitialAd - %@", name);
        
        
        [linker closeInterstitialForUnityName:name];
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays a video ad
    void SuperAwesomeUnitySAVideoAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnitySAVideoAd - %@", name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"type\":\"%@\"}", unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker showVideoAdWith:placementId
                      andAdJson:json
                   andUnityName:name
             andHasParentalGate:isParentalGateEnabled
              andHasCloseButton:shouldShowCloseButton
                 andClosesAtEnd:shouldAutomaticallyCloseAtEnd];
    }
    
    //
    // function that closes a fullscreen ad
    void SuperAwesomeUnityCloseSAFullscreenVideoAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        
        // updat-eeeeed!
        SAUnityLinker *linker = [[SAUnityLinker alloc] init];
        
        NSLog(@"SuperAwesomeUnityCloseSAFullscreenVideoAd - %@", name);
        
        [linker closeFullscreenVideoForUnityName:name];
    }
}