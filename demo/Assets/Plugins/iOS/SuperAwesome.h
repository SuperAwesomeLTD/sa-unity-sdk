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

/// also works as public headers

// import model header files
#import "SAResponse.h"
#import "SAAd.h"
#import "SACreative.h"
#import "SADetails.h"
#import "SAMedia.h"
#import "SATracking.h"

// import views
#import "SABannerAd.h"
#import "SAInterstitialAd.h"
#import "SAVideoAd.h"
#import "SAAppWall.h"

// load callback header
#import "SACallback.h"
#import "SAOrientation.h"

// @brief:
// This is the main SuperAwesome class that handles the Ad Session
// as a singleton (enable / disable test mode, configuration, version, etc)
@interface SuperAwesome : NSObject

// singleton instance (instead of init)
+ (instancetype) getInstance;

// get the dau id and the version
- (NSString*) getSdkVersion;
- (void) handleCPI;

@end
