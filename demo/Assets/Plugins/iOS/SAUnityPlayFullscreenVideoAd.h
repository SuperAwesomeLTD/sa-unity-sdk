//
//  SAUnityPlayFullscreenVideoAd.h
//  Pods
//
//  Created by Gabriel Coman on 13/05/2016.
//
//

#import <Foundation/Foundation.h>
#import "SAUnityExtension.h"

@interface SAUnityPlayFullscreenVideoAd : SAUnityExtension

// show a video ad
- (void) showVideoAdWith:(NSInteger)placementId
               andAdJson:(NSString*)adJson
            andUnityName:(NSString*)unityAd
      andHasParentalGate:(BOOL)isParentalGateEnabled
       andHasCloseButton:(BOOL)shouldShowCloseButton
          andClosesAtEnd:(BOOL)shouldAutomaticallyCloseAtEnd
 andShouldShowSmallClick:(BOOL)shouldShowSmallClickButton
           andShouldLock:(BOOL)shouldLockOrientation
         lockOrientation:(NSUInteger)lockOrientation;

// close the fullscreen video
- (void) closeFullscreenVideoForUnityName:(NSString*)unityAd;

@end
