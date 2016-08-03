//
//  SABaseObject.h
//  Pods
//
//  Created by Gabriel Coman on 12/06/2016.
//
//

#import <Foundation/Foundation.h>
#import "SADeserializationProtocol.h"
#import "SASerializationProtocol.h"

@interface SABaseObject : NSObject
@end

@interface SABaseObject (SAJson) <SADeserializationProtocol, SASerializationProtocol>
@end