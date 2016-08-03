//
//  NSDictionary+SAJsonExtension.h
//  Pods
//
//  Created by Gabriel Coman on 27/05/2016.
//
//

// import foundation
#import <Foundation/Foundation.h>

// import protocols
#import "SADeserializationProtocol.h"
#import "SASerializationProtocol.h"

/**
 *  Extension to NSDictionary to add serializaiton and deserializaiton functions
 */
@interface NSDictionary (SAJson) <SASerializationProtocol, SADeserializationProtocol>
@end
