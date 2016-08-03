//
//  SuperAwesome.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 28/09/2015.
//
//

#import <Foundation/Foundation.h>

// import system
#import "SAUtils.h"

// import model header files
#import "SAAd.h"
#import "SACreative.h"
#import "SADetails.h"
#import "SAData.h"

// import load
#import "SALoader.h"

// import views
#import "SABannerAd.h"
#import "SAInterstitialAd.h"
#import "SAVideoAd.h"
#import "SAFullscreenVideoAd.h"

// load protocols
#import "SAProtocols.h"

typedef enum SAConfiguration {
    STAGING = 0,
    DEVELOPMENT = 1,
    PRODUCTION = 2
} SAConfiguration;

// @brief:
// This is the main SuperAwesome class that handles the Ad Session
// as a singleton (enable / disable test mode, configuration, version, etc)
@interface SuperAwesome : NSObject

// singleton instance (instead of init)
+ (instancetype)getInstance;

// current SDK version
- (NSString*) getSdkVersion;

// set configuration - which determines what URL will call for ads
// @production: https://ads.superawesome.tv
// @staging: https://ads.staging.superawesome.tv
// @development: https://ads.dev.superawesome.tv
- (void) setConfigurationProduction;
- (void) setConfigurationStaging;
- (NSString*) getBaseURL;
- (SAConfiguration) getConfiguration;

// enable or disable test mode
- (void) enableTestMode;
- (void) disableTestMode;
- (void) setTesting:(BOOL)enabled;
- (BOOL) isTestingEnabled;

// get the dau
- (NSUInteger) getDAUID;

@end
