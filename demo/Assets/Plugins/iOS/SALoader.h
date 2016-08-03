//
//  SALoader.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 11/10/2015.
//
//

#import <Foundation/Foundation.h>
#import "SAUtils.h"

// predef classes
@class SAAd;

////////////////////////////////////////////////////////////////////////////////
// SALoader protocol
////////////////////////////////////////////////////////////////////////////////

// @brief:
// SALoader protocol defines two main optional functions that a user might
// implement if he wants to preload Ads
// This protocol is implemented by a SALoader class delegate
@protocol SALoaderProtocol <NSObject>

// main protocol functions
@optional

// @brief: function that gets called when an Ad is succesfully called
// @return: returns a valid SAAd object
- (void) didLoadAd:(SAAd*)ad;

// @brief: function that gets called when an Ad has failed to load
// @return: it returns a placementId of the failing ad through callback
- (void) didFailToLoadAdForPlacementId:(NSInteger)placementId;

@end

////////////////////////////////////////////////////////////////////////////////
// SALoader main class
////////////////////////////////////////////////////////////////////////////////

// @brief:
// This is a loader class that acts as Master loading class
// It's main purpose if to be called either by the SDK user or by SAAd
// to perform loading and preloading of ads
//
// It can preload ads using the SALoaderProtocol and - (void) preloadAdForPlacementId:
//
// The direct ad loading functions are hidden from the user
//
@interface SALoader : NSObject

// set delegate function
@property (nonatomic, weak) id<SALoaderProtocol> delegate;

// laod ad function;
// does not return anything directly, but via the SALoaderProtocol delegate
- (void) loadAdForPlacementId:(NSInteger)placementId;

@end
