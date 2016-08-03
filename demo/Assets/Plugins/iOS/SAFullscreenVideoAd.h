//
//  SAFullscreenVideoAd2.h
//  Pods
//
//  Created by Gabriel Coman on 13/02/2016.
//
//

#import <UIKit/UIKit.h>

// useful imports
#import "SAProtocols.h"

@class SAAd;
@class SAVideoAd;

@interface SAFullscreenVideoAd : UIViewController <SAViewProtocol>

@property (nonatomic, weak) id<SAAdProtocol> adDelegate;
@property (nonatomic, weak) id<SAParentalGateProtocol> parentalGateDelegate;
@property (nonatomic, weak) id<SAVideoAdProtocol> videoDelegate;
@property (nonatomic, assign) BOOL isParentalGateEnabled;
@property (nonatomic, assign) BOOL shouldAutomaticallyCloseAtEnd;
@property (nonatomic, assign) BOOL shouldShowCloseButton;
@property (nonatomic, assign) BOOL shouldLockOrientation;
@property (nonatomic, assign) BOOL shouldShowSmallClickButton;
@property (nonatomic, assign) NSUInteger lockOrientation;

@end
