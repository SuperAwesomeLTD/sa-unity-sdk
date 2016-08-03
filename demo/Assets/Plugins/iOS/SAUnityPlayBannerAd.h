//
//  SAUnityPlayBannerAd.h
//  Pods
//
//  Created by Gabriel Coman on 13/05/2016.
//
//

#import <Foundation/Foundation.h>
#import "SAUnityExtension.h"

@interface SAUnityPlayBannerAd : SAUnityExtension

// show a banner ad
- (void) showBannerAdWith:(NSInteger)placementId
                andAdJson:(NSString*)adJson
             andUnityName:(NSString*)unityAd
              andPosition:(NSInteger)position
                  andSize:(NSInteger)size
                 andColor:(NSInteger)color
       andHasParentalGate:(BOOL)isParentalGateEnabled;

// remove the banner ad
- (void) removeBannerForUnityName:(NSString*)unityAd;

@end
