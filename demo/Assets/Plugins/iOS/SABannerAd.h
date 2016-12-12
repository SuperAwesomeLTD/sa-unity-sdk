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
- (void) enableParentalGate;
- (void) disableParentalGate;
- (void) enableTestMode;
- (void) disableTestMode;
- (void) setConfigurationProduction;
- (void) setConfigurationStaging;
- (void) setColorTransparent;
- (void) setColorGray;

- (void) setTestMode: (BOOL) value;
- (void) setParentalGate: (BOOL) value;
- (void) setConfiguration: (NSInteger) value;
- (void) setColor: (BOOL) value;

@end
