//
//  SABannerAd2.h
//  Pods
//
//  Created by Gabriel Coman on 13/02/2016.
//
//

#import <UIKit/UIKit.h>

@class SAAd;

// useful imports
#import "SACallback.h"
#import "SASession.h"

// class declaration for SABannerAd
@interface SABannerAd : UIView

// "action" methods
- (void) load:(NSInteger)placementId;
- (void) play;
- (BOOL) hasAdAvailable;
- (void) close;
- (void) resize:(CGRect)toframe;

// public "state" setters
- (void) setCallback:(sacallback)callback;
- (void) setIsParentalGateEnabled:(BOOL)isParentalGateEnabled;
- (void) setTest:(BOOL) isTest;
- (void) setTestEnabled;
- (void) setTestDisabled;
- (void) setConfiguration: (NSInteger) config;
- (void) setConfigurationProduction;
- (void) setConfigurationStaging;

@end
