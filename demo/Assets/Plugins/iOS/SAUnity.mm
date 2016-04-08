//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"
#import "SAUnityLinker.h"

extern "C" {
    
    NSMutableDictionary *linkerDict = [[NSMutableDictionary alloc] init];
    
    //
    // Setter / getter and remover functions for linker dictionary objects
    // @warn: this should be compatible with both Unity 4- and Unity 5+
    SAUnityLinker *getOrCreateLinker(NSString *name) {
        SAUnityLinker *linker = [linkerDict objectForKey:name];
        if (linker == nil) {
            linker = [[SAUnityLinker alloc] init];
            [linkerDict setObject:linker forKey:name];
        }
        return linker;
    }
    
    void removeLinker(NSString *name){
        if ([linkerDict objectForKey:name]){
            [linkerDict removeObjectForKey:name];
        }
        NSLog(@"Remaining: %@", keys);
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and loads an Ad with the help of the SuperAwesome iOS SDK
    void SuperAwesomeUnityLoadAd(const char *unityName, int placementId, BOOL isTestingEnabled, int config) {
        // transfrom the name
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSLog(@"SuperAwesomeUnityLoadAd - %@", name);
        
        SAConfiguration iconfig = (SAConfiguration)config;
        switch (iconfig) {
            case PRODUCTION: [[SuperAwesome getInstance] setConfigurationProduction]; break;
            case STAGING: [[SuperAwesome getInstance] setConfigurationStaging]; break;
            case DEVELOPMENT: [[SuperAwesome getInstance] setConfigurationDevelopment]; break;
            default: break;
        }
        
        // create a linker
        SAUnityLinker *linker = getOrCreateLinker(name);
        
        // assign the success and error callbacks
        linker.loadingEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback, NSString *adString) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\", \"adJson\":%@}", placementId, unityCallback, adString];
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
    void SuperAwesomeUnitySABannerAd(int placementId, const char *adJson, const char *unityName, int position, int size, int color, BOOL isParentalGateEnabled) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        NSLog(@"SuperAwesomeUnitySABannerAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [linker showBannerAdWith:placementId
                       andAdJson:json
                    andUnityName:name
                     andPosition:position
                         andSize:size
                        andColor:color
              andHasParentalGate:isParentalGateEnabled];
    }
    
    //
    // function that removes a banner ad
    void SuperAwesomeUnityRemoveSABannerAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSLog(@"SuperAwesomeUnityRemoveSABannerAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        [linker removeBannerForUnityName:name];
        removeLinker(name);
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays an interstitial ad
    void SuperAwesomeUnitySAInterstitialAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        NSLog(@"SuperAwesomeUnitySAInterstitialAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
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
        NSLog(@"SuperAwesomeUnityCloseSAInterstitialAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        [linker closeInterstitialForUnityName:name];
        removeLinker(name);
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays a video ad
    void SuperAwesomeUnitySAVideoAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        NSLog(@"SuperAwesomeUnitySAVideoAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        
        // add callbacks
        linker.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
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
        NSLog(@"SuperAwesomeUnityCloseSAFullscreenVideoAd - %@", name);
        
        // updat-eeeeed!
        SAUnityLinker *linker = getOrCreateLinker(name);
        [linker closeFullscreenVideoForUnityName:name];
        removeLinker(name);
    }
}