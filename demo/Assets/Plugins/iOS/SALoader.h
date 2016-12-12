//
//  SALoader.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 11/10/2015.
//
//

#import <Foundation/Foundation.h>

// predef classes
@class SAAd;
@class SASession;
@class SAResponse;

// callback
typedef void (^didLoadAd)(SAResponse *response);

// class
@interface SALoader : NSObject

/**
 *  Method that loads an ad async
 *
 *  @param placementId the placement id
 *  @param session     the session object that should contain base url, version, etc
 *  @param result      the result callback
 */
- (void) loadAd:(NSInteger)placementId
    withSession:(SASession*)session
      andResult:(didLoadAd)result;

@end
