//
//  SAUnityLinker.h
//  Pods
//
//  Created by Gabriel Coman on 21/01/2016.
//
//

#import <Foundation/Foundation.h>

// callback for generic success with data
typedef void (^adEvent)(NSString *unityAd, int placementId, NSString *unityCallback);

// callback for generic success with data
typedef void (^loadingEvent)(NSString *unityAd, int placementId, NSString *unityCallback, NSString *adString);

@interface SAUnityExtension : NSObject

// callback variables
@property (nonatomic, assign) loadingEvent loadingEvent;
@property (nonatomic, assign) adEvent adEvent;

@end
