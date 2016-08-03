//
//  SADetails.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 28/09/2015.
//
//

#import <Foundation/Foundation.h>
#import "SAJsonParser.h"

//
// forward declaration of ad data
@class SAData;

// @brief:
// The SADetails class contains fine grained information about the creative
// of an ad (such as width, iamge, vast, tag, etc)
// Depending on the format of the creative, some fields are essential,
// and some are optional
//
// This dependency is regulated by SAValidator.h
@interface SADetails : SABaseObject <SASerializationProtocol, SADeserializationProtocol>

// the width & height of the creative; can be applied to images, banners,
// rich-media, etc
// there are cases when this is 1 x 1 - which indicates a relative-size
// creative
@property (nonatomic, assign) NSInteger width;
@property (nonatomic, assign) NSInteger height;

// in case creative format is image_with_link, this is the URL of the image
@property (nonatomic, strong) NSString *image;

// aux value needed when sending ad data like rating and such
@property (nonatomic, assign) NSInteger value;

// name of the creative
@property (nonatomic, strong) NSString *name;

// in case creative format is video, this is the URL of the video to be streamed
@property (nonatomic, strong) NSString *video;

// in case creative format is video, this is the video bitrate
@property (nonatomic, assign) NSInteger bitrate;

// in case creative format is video, this is the total duration
@property (nonatomic, assign) NSInteger duration;

// in case creative format is video, this is the associated vast tag
@property (nonatomic, strong) NSString *vast;

// in case creative format is tag, this is the JS tag
@property (nonatomic, strong) NSString *tag;

// in case creative format is rich media, this is the URL to the zip with all
// media resources; at the moment it's not used, but could be used when doing
// truly preloaded ads
@property (nonatomic, strong) NSString *zipFile;

// in case creative format is rich media, this is the URL of the rich media
@property (nonatomic, strong) NSString *url;

// this is the placement format, defined in SAPlacementFormat.h
// as of now, it's kind of useless
@property (nonatomic, strong) NSString *placementFormat;

// the CDN url
@property (nonatomic, strong) NSString *cdnUrl;

// the ad's data (either HTML, or video, etc)
@property (nonatomic, strong) SAData *data;

@end
