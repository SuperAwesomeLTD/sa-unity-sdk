//
//  SAUnity.mm
//
//  Created by Connor Leigh-Smith on 11/08/15.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

extern "C" {
    
    void SuperAwesomeUnityOpenVideoAd(const char* placementID)
    {
        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSLog(@"Unity requested video ad %@", placementIDString);
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithPlacementID:placementIDString];
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rvc presentViewController:vc animated:YES completion:nil];
    }
    
    void SuperAwesomeUnityOpenVideoAdTestmode(const char* placementID)
    {
        [[SuperAwesome sharedManager] setTestModeEnabled:YES];
        NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSLog(@"Unity requested video ad in test mode %@", placementIDString);
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithPlacementID:placementIDString];
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rvc presentViewController:vc animated:YES completion:nil];
    }
    
    void SuperAwesomeUnityOpenParentalGate(const char *adName) {
        SAParentalGate *gate = [[SAParentalGate alloc] init];
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