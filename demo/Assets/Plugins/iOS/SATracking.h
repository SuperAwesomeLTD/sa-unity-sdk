//
//  SATracking.h
//  Pods
//
//  Created by Gabriel Coman on 22/08/2016.
//
//

#import "SAJsonParser.h"

@interface SATracking : SABaseObject <SADeserializationProtocol, SASerializationProtocol>
@property (nonatomic, strong) NSString *event;
@property (nonatomic, strong) NSString *URL;
@end
