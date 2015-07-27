//
//  SAInterstitialView.h
//  SAMobileSDK
//
//  Created by Balázs Kiss on 12/08/14.
//  Copyright (c) 2014 SuperAwesome Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "SAPlacementView.h"
#import "SAParentalGate.h"

/**
 * Defines the interface the delegate of an interstitial ads
 */
@class SAParentalGateOpener;

@interface SAParentalGateOpener : NSObject 

- (instancetype)initWithUrl:(NSString *)urlString;

- (BOOL)openGate;


@end
