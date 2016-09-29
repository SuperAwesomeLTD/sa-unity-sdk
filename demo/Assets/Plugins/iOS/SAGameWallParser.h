//
//  SAGameWallParser.h
//  Pods
//
//  Created by Gabriel Coman on 28/09/2016.
//
//

#import <Foundation/Foundation.h>

@class SAAd;

// callback
typedef void (^gotAllImages)();

@interface SAGameWallParser : NSObject

- (void) getGameWallResourcesForAds:(NSArray <SAAd*> *) ads andCallback:(gotAllImages) callback;

@end
