//
//  SAMediaFile.h
//  Pods
//
//  Created by Gabriel Coman on 09/03/2016.
//
//

#import <Foundation/Foundation.h>
#import "SAJsonParser.h"

//
// @brief: media file implementation
@interface SAVASTMediaFile : SABaseObject <SASerializationProtocol, SADeserializationProtocol>
@property (nonatomic, strong) NSString *width;
@property (nonatomic, strong) NSString *height;
@property (nonatomic, strong) NSString *type;
@property (nonatomic, strong) NSString *URL;
@property (nonatomic, strong) NSString *diskURL;
@end