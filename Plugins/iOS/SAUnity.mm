//
//  SAUnity.mm
//
//  Created by Balázs Kiss on 17/02/15.
//
//


#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

extern "C" {
    
    void SuperAwesomeUnityMethod()
    {
        SAVideoAdViewController *vc = [[SAVideoAdViewController alloc] initWithAppID:@"14" placementID:@"314228"];
        UIViewController *rvc = [UIApplication sharedApplication].keyWindow.rootViewController;
        [rvc presentViewController:vc animated:YES completion:nil];
    }
    
}