//
//  SAResponse.h
//  Pods
//
//  Created by Gabriel Coman on 27/09/2016.
//
//

#import <Foundation/Foundation.h>

#import "SAJsonParser.h"
#import "SACreativeFormat.h"

@class SAAd;

@interface SAResponse : SABaseObject <SASerializationProtocol, SADeserializationProtocol>

@property (nonatomic, assign) NSInteger placementId;
@property (nonatomic, assign) SACreativeFormat format;
@property (nonatomic, assign) NSInteger status;
@property (nonatomic, strong) NSMutableArray<SAAd*> *ads;

@end
