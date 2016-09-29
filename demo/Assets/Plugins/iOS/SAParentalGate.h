//
//  SAParentalGate2.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 28/09/2015.
//
//

#import <UIKit/UIKit.h>

// forward declarations
@class SAAd;

// define a block used by UIAlertActions
typedef void(^actionBlock) (UIAlertAction *action);

// interface
@interface SAParentalGate : NSObject <UIAlertViewDelegate>

// custom init functions
- (id) initWithWeakRefToView:(id)weakRef andAd:(SAAd*)ad;
- (id) initWithWeakRefToView:(id)weakRef andAd:(SAAd *)ad andPosition:(NSInteger)position;

// show function
- (void) show;

// close function
- (void) close;

@end
