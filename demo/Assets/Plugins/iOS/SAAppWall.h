//
//  SAGameWall.h
//  Pods
//
//  Created by Gabriel Coman on 27/09/2016.
//
//

#import <UIKit/UIKit.h>
#import "SACallback.h"

@interface SAAppWall : UIViewController

// static "action" methods
+ (void) load:(NSInteger) placementId;
+ (void) play:(NSInteger) placementId fromVC:(UIViewController*)parent;
+ (BOOL) hasAdAvailable: (NSInteger) placementId;

// static "state" methods
+ (void) setCallback:(sacallback)call;
+ (void) enableTestMode;
+ (void) disableTestMode;
+ (void) enableParentalGate;
+ (void) disableParentalGate;
+ (void) setConfigurationProduction;
+ (void) setConfigurationStaging;

+ (void) setTestMode: (BOOL) value;
+ (void) setParentalGate: (BOOL) value;
+ (void) setConfiguration: (NSInteger) value;


@end
