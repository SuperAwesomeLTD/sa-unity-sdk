//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

extern "C" {
    
    void SuperAwesomeUnityOpenVideoAd(const char* placementID, BOOL gateEnabled, BOOL testMode)
    {
        [[SuperAwesome sharedManager] setTestModeEnabled:testMode];
        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSLog(@"Unity requested video ad %@", placementIDString);
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithPlacementId:placementIDString];
        vc.parentalGateEnabled = gateEnabled;
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rvc presentViewController:vc animated:YES completion:nil];
    }
    
//    void SuperAwesomeUnityOpenVideoAdTestmode(const char* placementID)
//    {
//        [[SuperAwesome sharedManager] setTestModeEnabled:YES];
//        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
//        NSLog(@"Unity requested video ad in test mode %@", placementIDString);
//        SAVideoAdViewController2 *vc = [[SAVideoAdViewController2 alloc] initWithPlacementId:placementIDString];
//        vc.parentalGateEnabled = gateEnabled;
//        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
//        [rvc presentViewController:vc animated:YES completion:nil];
//    }
    
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