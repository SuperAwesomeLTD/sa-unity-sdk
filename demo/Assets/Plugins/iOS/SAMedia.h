//
//  SAMedia.h
//  Pods
//
//  Created by Gabriel Coman on 22/08/2016.
//
//

#import "SAJsonParser.h"

@interface SAMedia : SABaseObject <SASerializationProtocol, SADeserializationProtocol>
@property (nonatomic, strong) NSString *html;
@property (nonatomic, strong) NSString *playableDiskUrl;
@property (nonatomic, strong) NSString *playableMediaUrl;
@property (nonatomic, strong) NSString *type;
@property (nonatomic, assign) NSInteger bitrate;
@property (nonatomic, assign) BOOL isOnDisk;
@end
