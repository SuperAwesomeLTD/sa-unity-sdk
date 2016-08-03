//
//  SAAd.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 28/09/2015.
//
//

#import <Foundation/Foundation.h>

#import "SAJsonParser.h"

// formward declarations
@class SACreative;

// @brief:
// This model class contains all information that is received from the server
// when an Ad is requested, as well as some aux fields that will be generated
// by the SDK
@interface SAAd : SABaseObject <SASerializationProtocol, SADeserializationProtocol>

// the SA server can send an error; if that's the case, this field will not be nill
@property (nonatomic, assign) NSInteger error;

// the advertiser Id
@property (nonatomic, assign) NSInteger advertiserId;

// the publisher Id
@property (nonatomic, assign) NSInteger publisherId;

// the App id
@property (nonatomic, assign) NSInteger app;

// the ID of the placement that the ad was sent for
@property (nonatomic, assign) NSInteger placementId;

// line item
@property (nonatomic, assign) NSInteger lineItemId;

// the ID of the campaign that the ad is a part of
@property (nonatomic, assign) NSInteger campaignId;

// is true when the ad is a test ad
@property (nonatomic, assign) BOOL test;

// is true when ad is fallback (fallback ads are sent when there are no
// real ads to display for a certain placement)
@property (nonatomic, assign) BOOL isFallback;
@property (nonatomic, assign) BOOL isFill;
@property (nonatomic, assign) BOOL isHouse;
@property (nonatomic, assign) BOOL safeAdApproved;
@property (nonatomic, assign) BOOL showPadlock;

// pointer to the creative data associated with the ad
@property (nonatomic, strong) SACreative *creative;

@end
