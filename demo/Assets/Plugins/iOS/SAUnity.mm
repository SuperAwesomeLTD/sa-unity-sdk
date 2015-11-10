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
        [[SuperAwesome sharedManager] setTestModeEnabled:testMode];
        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSLog(@"Unity requested video ad %@", placementIDString);
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithPlacementId:placementIDString];
        vc.parentalGateEnabled = gateEnabled;
        vc.view.backgroundColor = [UIColor blackColor];
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        
        vc.delegate = nil;
        NSString *name = [NSString stringWithUTF8String:adName];
        [vc setAdName:name];
        [vc addLoadVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdLoaded", "");
        }];
        [vc addFailToLoadVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdFailedToLoad", "");
        }];
        [vc addStartVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdStartedPlaying", "");
        }];
        [vc addStopVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdStoppedPlaying", "");
        }];
        [vc addFailToPlayVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdFailedToPlay", "");
        }];
        [vc addClickVideoBlock:^(NSString *adname) {
            UnitySendMessage([adname UTF8String], "videoAdClicked", "");
        }];
        
        [rvc presentViewController:vc animated:YES completion:nil];
    }
    
    void SuperAwesomeUnityOpenParentalGate(const char *adName, const char *placementID, long creativeId, long lineItemId) {
        
        NSString *_placementId = [NSString stringWithUTF8String:placementID];
        NSString *_lineItemId = [NSString stringWithFormat:@"%ld", lineItemId];
        NSString *_creativeId = [NSString stringWithFormat:@"%ld", creativeId];;
        
        // init the gate
        SAParentalGate *gate = [[SAParentalGate alloc] initWithPlacementId:_placementId
                                                             andCreativeId:_creativeId
                                                             andLineItemId:_lineItemId];
        gate.delegate = nil;
        NSString *name = [NSString stringWithUTF8String:adName];
        [gate setAdName:name];
        [gate addSuccessBlock:^(NSString *adname){
            // go to add
            NSLog(@"AD: %@ requests goto URL", adname);
            UnitySendMessage([adname UTF8String], "goDirectlyToAdURL", "");
        }];
        [gate addErrorBlock:^(NSString *adname){
            // do nothing here really
        }];
        [gate addCancelBlock:^(NSString *adname){
            // do nothing here really
        }];
        [gate show];
    }
    
    void SuperAwesomeUnityShowPadlockView(){
        // do nothing so far
        SAPadlockView *pad = [[SAPadlockView alloc] init];
        [[[[UIApplication sharedApplication] delegate] window] addSubview:pad];
    }
}