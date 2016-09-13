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
+ (void) setIsParentalGateEnabled: (BOOL) value;
+ (void) setShouldAutomaticallyCloseAtEnd: (BOOL) value;
+ (void) setShouldShowCloseButton: (BOOL) value;
+ (void) setShouldLockOrientation: (BOOL) value;
+ (void) setShouldShowSmallClickButton: (BOOL) value;
+ (void) setLockOrientation: (NSUInteger) value;
+ (void) setTest:(BOOL) isTest;
+ (void) setTestEnabled;
+ (void) setTestDisabled;
+ (void) setConfiguration: (NSInteger) config;
+ (void) setConfigurationProduction;
+ (void) setConfigurationStaging;

@end
