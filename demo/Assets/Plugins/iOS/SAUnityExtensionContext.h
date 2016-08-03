//
//  SAUnityLinkerManager.h
//  Pods
//
//  Created by Gabriel Coman on 04/02/2016.
//
//

#import <Foundation/Foundation.h>
#import "SuperAwesome.h"

@interface SAUnityExtensionContext : NSObject

// singleton instance (instead of init)
+ (SAUnityExtensionContext *)getInstance;

// setters and getters
- (void) setAd:(NSObject*)adView forKey:(NSString*)key;
- (NSObject*) getAdForKey:(NSString*)key;
- (NSString*) getKeyForId:(NSInteger)placementId;
- (void) removeAd:(NSInteger)placementId;

@end
