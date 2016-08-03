//
//  SATracking.h
//  Pods
//
//  Created by Gabriel Coman on 09/03/2016.
//
//

#import <Foundation/Foundation.h>
#import "SAJsonParser.h"

//
// @brief: the tracking object
@interface SAVASTTracking : SABaseObject <SADeserializationProtocol, SASerializationProtocol>
@property (nonatomic, strong) NSString *event;
@property (nonatomic, strong) NSString *URL;
@end
