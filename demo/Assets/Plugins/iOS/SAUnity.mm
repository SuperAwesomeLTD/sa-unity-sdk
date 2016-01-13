//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

extern "C" {
    
    void SuperAwesomeUnityOpenVideoAd(const char *adName, const char* placementID, BOOL gateEnabled, BOOL testMode)
    {
        // set staging config
        // [[SuperAwesome getInstance] setConfigurationStaging];
        
        // parse sent data
        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSString *name = [NSString stringWithUTF8String:adName];
        BOOL isTest = testMode;
        BOOL isParentalGate = gateEnabled;
        
        // start the linker
        SAFullscreenVideoAdUnityLinker *linker = [[SAFullscreenVideoAdUnityLinker alloc] initWithVideoAd:[placementIDString intValue]
                                                                                            andUnityName:name
                                                                                                withGate:isParentalGate
                                                                                              inTestMode:isTest
                                                                                          hasCloseButton:true
                                                                                          andClosesAtEnd:true];
        [linker addLoadVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdLoaded", "");
        }];
        [linker addFailToLoadVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdFailedToLoad", "");
        }];
        [linker addStartVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdStartedPlaying", "");
        }];
        [linker addStopVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdStoppedPlaying", "");
        }];
        [linker addClickVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdClicked", "");
        }];
        [linker addFailToPlayVideoBlock:^(NSString *unityAd) {
            UnitySendMessage([unityAd UTF8String], "videoAdFailedToPlay", "");
        }];
        
        // start!!!!
        [linker start];
    }
    
    void SuperAwesomeUnityOpenParentalGate(const char *adName, const char *placementID, long creativeId, long lineItemId) {
        
//        NSString *_placementId = [NSString stringWithUTF8String:placementID];
//        NSString *_lineItemId = [NSString stringWithFormat:@"%ld", lineItemId];
//        NSString *_creativeId = [NSString stringWithFormat:@"%ld", creativeId];;
//        
//        // init the gate
//        SAParentalGate *gate = [[SAParentalGate alloc] initWithPlacementId:_placementId
//                                                             andCreativeId:_creativeId
//                                                             andLineItemId:_lineItemId];
//        gate.delegate = nil;
//        NSString *name = [NSString stringWithUTF8String:adName];
//        [gate setAdName:name];
//        [gate addSuccessBlock:^(NSString *adname){
//            // go to add
//            NSLog(@"AD: %@ requests goto URL", adname);
//            UnitySendMessage([adname UTF8String], "goDirectlyToAdURL", "");
//        }];
//        [gate addErrorBlock:^(NSString *adname){
//            // do nothing here really
//        }];
//        [gate addCancelBlock:^(NSString *adname){
//            // do nothing here really
//        }];
//        [gate show];
    }
    
    void SuperAwesomeUnityShowPadlockView(){
        // do nothing so far
//        SAPadlockView *pad = [[SAPadlockView alloc] init];
//        [[[[UIApplication sharedApplication] delegate] window] addSubview:pad];
    }
}