//
//  SAVideoAd2.h
//  Pods
//
//  Created by Gabriel Coman on 01/09/2016.
//
//

#import <UIKit/UIKit.h>
#import "SACallback.h"

@interface SAVideoAd : UIViewController

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
+ (void) setOrientationAny;
+ (void) setOrientationPortrait;
+ (void) setOrientationLandscape;
+ (void) enableCloseButton;
+ (void) disableCloseButton;
+ (void) enableSmallClickButton;
+ (void) disableSmallClickButton;
+ (void) enableCloseAtEnd;
+ (void) disableCloseAtEnd;

@end