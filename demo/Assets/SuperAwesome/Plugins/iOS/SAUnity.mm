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

}