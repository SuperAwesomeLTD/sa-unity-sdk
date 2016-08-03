//
//  SAUnity.mm
//  Pods
//
//  Created by Gabriel Coman on 19/07/2016.
//
//

//
//  SAUnity.c
//  Pods
//
//  Created by Gabriel Coman on 24/06/2016.
//
//

//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
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
    
    NSMutableDictionary *extensionDict = [[NSMutableDictionary alloc] init];
    
    //
    // Setter / getter and remover functions for linker dictionary objects
    // @warn: this should be compatible with both Unity 4- and Unity 5+
    SAUnityExtension *getOrCreateExtension(NSString *name) {
        return [extensionDict objectForKey:name];
    }
    
    void removeExtension(NSString *name){
        if ([extensionDict objectForKey:name]){
            [extensionDict removeObjectForKey:name];
        }
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
            case DEVELOPMENT: [[SuperAwesome getInstance] setConfigurationStaging]; break;
            default: break;
        }
        
        // create a linker
        SAUnityLoadAd *extension = (SAUnityLoadAd*)getOrCreateExtension(name);
        if (!extension) {
            extension = [[SAUnityLoadAd alloc] init];
            [extensionDict setObject:extension forKey: name];
        }
        
        // assign the success and error callbacks
        extension.loadingEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback, NSString *adString) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\", \"adJson\":%@}", placementId, unityCallback, adString];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // call to load
        [extension loadAd:placementId
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
        SAUnityPlayBannerAd *extension = (SAUnityPlayBannerAd*)getOrCreateExtension(name);
        if (!extension) {
            extension = [[SAUnityPlayBannerAd alloc] init];
            [extensionDict setObject:extension forKey: name];
        }
        
        // add callbacks
        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [extension showBannerAdWith:placementId
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
        SAUnityPlayBannerAd *extension = (SAUnityPlayBannerAd*)getOrCreateExtension(name);
        if (extension) {
            [extension removeBannerForUnityName:name];
            removeExtension(name);
        }
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays an interstitial ad
    void SuperAwesomeUnitySAInterstitialAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldLockOrientation, int lockOrientation) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        NSLog(@"SuperAwesomeUnitySAInterstitialAd - %@", name);
        
        NSUInteger orientation = UIInterfaceOrientationMaskAll;
        switch (lockOrientation) {
            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
        }
        
        // updat-eeeeed!
        SAUnityPlayInterstitialAd *extension = (SAUnityPlayInterstitialAd*)getOrCreateExtension(name);
        if (!extension) {
            extension = [[SAUnityPlayInterstitialAd alloc] init];
            [extensionDict setObject:extension forKey: name];
        }
        
        // add callbacks
        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [extension showInterstitialAdWith:placementId
                                andAdJson:json
                             andUnityName:name
                       andHasParentalGate:isParentalGateEnabled
                            andShouldLock:shouldLockOrientation
                          lockOrientation:orientation];
    }
    
    //
    // function that removes an interstitial ad
    void SuperAwesomeUnityCloseSAInterstitialAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSLog(@"SuperAwesomeUnityCloseSAInterstitialAd - %@", name);
        
        // updat-eeeeed!
        SAUnityPlayInterstitialAd *extension = (SAUnityPlayInterstitialAd*)getOrCreateExtension(name);
        if (extension) {
            [extension closeInterstitialForUnityName:name];
            removeExtension(name);
        }
    }
    
    //
    // This function acts as a bridge between Unity-iOS-Unity
    // and displays a video ad
    void SuperAwesomeUnitySAVideoAd(int placementId, const char *adJson, const char *unityName, BOOL isParentalGateEnabled, BOOL shouldShowCloseButton, BOOL shouldAutomaticallyCloseAtEnd, BOOL shouldShowSmallClickButton, BOOL shouldLockOrientation, int lockOrientation) {
        
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSString *json = [NSString stringWithUTF8String:adJson];
        NSLog(@"SuperAwesomeUnitySAVideoAd - %@", name);
        
        NSUInteger orientation = UIInterfaceOrientationMaskAll;
        switch (lockOrientation) {
            case 1: orientation = UIInterfaceOrientationMaskPortrait; break;
            case 2: orientation = UIInterfaceOrientationMaskLandscape; break;
        }
        
        // updat-eeeeed!
        SAUnityPlayFullscreenVideoAd *extension = (SAUnityPlayFullscreenVideoAd*)getOrCreateExtension(name);
        if (!extension) {
            extension = [[SAUnityPlayFullscreenVideoAd alloc] init];
            [extensionDict setObject:extension forKey: name];
        }
        
        // add callbacks
        extension.adEvent = ^(NSString *unityAd, int placementId, NSString *unityCallback) {
            NSString *payload = [NSString stringWithFormat:@"{\"placementId\":\"%d\", \"type\":\"%@\"}", placementId, unityCallback];
            UnitySendMessage([unityAd UTF8String], "nativeCallback", [payload UTF8String]);
        };
        
        // start
        [extension showVideoAdWith:placementId
                         andAdJson:json
                      andUnityName:name
                andHasParentalGate:isParentalGateEnabled
                 andHasCloseButton:shouldShowCloseButton
                    andClosesAtEnd:shouldAutomaticallyCloseAtEnd
           andShouldShowSmallClick:shouldShowSmallClickButton
                     andShouldLock:shouldLockOrientation
                   lockOrientation:orientation];
    }
    
    //
    // function that closes a fullscreen ad
    void SuperAwesomeUnityCloseSAFullscreenVideoAd(const char *unityName) {
        // parse parameters
        NSString *name = [NSString stringWithUTF8String:unityName];
        NSLog(@"SuperAwesomeUnityCloseSAFullscreenVideoAd - %@", name);
        
        // updat-eeeeed!
        SAUnityPlayFullscreenVideoAd *extension = (SAUnityPlayFullscreenVideoAd*)getOrCreateExtension(name);
        if (extension) {
            [extension closeFullscreenVideoForUnityName:name];
            removeExtension(name);
        }
    }
}