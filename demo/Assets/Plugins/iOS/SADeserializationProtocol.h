//
//  NSObject+JSONInterface.h
//  Pods
//
//  Created by Gabriel Coman on 26/05/2016.
//
//

#import <Foundation/Foundation.h>

/**
 This protocol describes the functions that should be implemented by a class
 in order to conform to deserialization
 */
@protocol SADeserializationProtocol <NSObject>

@required

/**
 *  Standard init function with a Dictionary
 *
 *  @param jsonDictionary the json dictionary to try to perform parsing from
 *
 *  @return an instance of the object
 */
- (id) initWithJsonDictionary:(NSDictionary*)jsonDictionary;

@optional

/**
 *  Init with JSON String; has default implementation
 *
 *  @param jsonString a json string
 *
 *  @return an instance of the object
 */
- (id) initWithJsonString:(NSString*)jsonString;

/**
 *  Init with JSON NSData object; has default implementation
 *
 *  @param jsonData json as NSData
 *
 *  @return an instance of the object
 */
- (id) initWithJsonData:(NSData*)jsonData;

@required

/**
 *  This function is used to validate that a model has been correctly 
 *  deserialized; Has a default implementation
 *
 *  @return true or false
 */
- (BOOL) isValid;

@end
