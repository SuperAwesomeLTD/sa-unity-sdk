//
//  SAVASTManager.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 12/15/2015.
//
//

#import <Foundation/Foundation.h>

// import vast stuff
#import "SAVASTParser.h"

@class SAVASTAd;
@class SAVideoPlayer;

////////////////////////////////////////////////////////////////////////////////
// The VASTManager main protocol
////////////////////////////////////////////////////////////////////////////////

// Functions used by the Manager to send out info
@protocol SAVASTManagerProtocol <NSObject>

@optional

// Called when the VAST data could not be parsed at all
- (void) didNotFindAds;

// Called when an Ad from a group of Ads has just started playing
- (void) didStartAd;

// Called when a creative from the current Ad has started playing
- (void) didStartCreative;

// Called when 1/4 of a creative's length has passed
- (void) didReachFirstQuartileOfCreative;

// Called when 1/2 of a creative's length has passed
- (void) didReachMidpointOfCreative;

// Called when 3/4 of a creative's length has passed
- (void) didReachThirdQuartileOfCreative;

// Called when a creative from the current Ad has finished playing
- (void) didEndOfCreative;

// Called when a creative could not be played (due to corrupt resource on the network, wifi problems, etc)
- (void) didHaveErrorForCreative;

// Called when an ad has played all its creatives
- (void) didEndAd;

// Called when all ads and all creative have played
- (void) didEndAllAds;

// Goto URL
- (void) didGoToURL:(NSURL*)url withTrackingArray:(NSArray*)clickTarcking;

@end

////////////////////////////////////////////////////////////////////////////////
// The VASTManager main class
////////////////////////////////////////////////////////////////////////////////

@interface SAVASTManager : NSObject

// delegate
@property (nonatomic, weak) id<SAVASTManagerProtocol> delegate;

// custom init
- (id) initWithPlayer:(SAVideoPlayer*)player;

// function to manage ads
- (void) manageWithAd:(SAVASTAd*)ad;

// parse VAST
- (void) parseVASTURL:(NSString*)urlString;

@end
