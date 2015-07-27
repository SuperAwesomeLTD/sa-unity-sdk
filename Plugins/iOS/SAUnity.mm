//
//  SAUnity.mm
//
//  Created by Balázs Kiss on 17/02/15.
//
//


#import <Foundation/Foundation.h>
#import "SuperAwesome.h"
#import "SAParentalGateOpener.h"

extern "C" {
    
    void SuperAwesomeUnityOpenVideoAd(int appID, const char* placementID)
    {
    	NSString *placementIDString = [NSString stringWithUTF8String: placementID];
        NSString *appIDString = [NSString stringWithFormat:@"%i", appID];
    	NSLog(@"Unity requested video ad %@ %@", appIDString, placementIDString);
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithAppID:appIDString placementID:placementIDString];
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rvc presentViewController:vc animated:YES completion:nil];
    }

    void SuperAwesomeUnityOpenParentalGate(const char* url) {
        NSString *urlString = [NSString stringWithUTF8String: url];
        SAParentalGateOpener *opener = [[SAParentalGateOpener alloc] initWithUrl:urlString];
        [opener openGate];
    }
    
}