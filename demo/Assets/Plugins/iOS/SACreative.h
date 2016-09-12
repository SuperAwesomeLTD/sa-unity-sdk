//
//  SACreative.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 28/09/2015.
//
//

#import <Foundation/Foundation.h>
#import "SAJsonParser.h"
#import "SATracking.h"

// creative format typedef
typedef NS_ENUM(NSInteger, SACreativeFormat) {
    invalid = -1,
    image = 0,
    video = 1,
    rich = 2,
    tag = 3
};

// forward declarations
@class SADetails;

// @brief:
// The creative contains essential ad information like format, click url
// and such
@interface SACreative : SABaseObject <SADeserializationProtocol, SASerializationProtocol>

@property (nonatomic, assign) NSInteger _id;
@property (nonatomic, strong) NSString *name;
@property (nonatomic, assign) NSInteger cpm;
@property (nonatomic, strong) NSString *format;
@property (nonatomic, assign) SACreativeFormat creativeFormat;
@property (nonatomic, assign) BOOL live;
@property (nonatomic, assign) BOOL approved;
@property (nonatomic, strong) NSString *customPayload;
@property (nonatomic, strong) NSString *clickUrl;
@property (nonatomic, strong) NSString *impressionUrl;
@property (nonatomic, strong) NSMutableArray<SATracking*> *events;

@property (nonatomic, strong) SADetails *details;

@end
