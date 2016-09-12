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

typedef NS_ENUM(NSInteger, SAVASTAdType) {
    InLine = 0,
    Wrapper = 1
};

// formward declarations
@class SACreative;

@interface SAAd : SABaseObject <SASerializationProtocol, SADeserializationProtocol>

@property (nonatomic, assign) NSInteger error;
@property (nonatomic, assign) NSInteger advertiserId;
@property (nonatomic, assign) NSInteger publisherId;
@property (nonatomic, assign) NSInteger app;
@property (nonatomic, assign) NSInteger lineItemId;
@property (nonatomic, assign) NSInteger campaignId;
@property (nonatomic, assign) NSInteger placementId;
@property (nonatomic, assign) BOOL test;
@property (nonatomic, assign) BOOL isFallback;
@property (nonatomic, assign) BOOL isFill;
@property (nonatomic, assign) BOOL isHouse;
@property (nonatomic, assign) BOOL safeAdApproved;
@property (nonatomic, assign) BOOL showPadlock;

@property (nonatomic, assign) BOOL isVAST;
@property (nonatomic, assign) SAVASTAdType vastType;
@property (nonatomic, strong) NSString *vastRedirect;

@property (nonatomic, strong) SACreative *creative;

- (void) sumAd:(SAAd*) dest;

@end
