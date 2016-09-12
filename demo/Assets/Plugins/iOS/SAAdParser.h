//
//  SAParser.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 11/10/2015.
//
//

#import <Foundation/Foundation.h>

// forward declarations
@class SAAd;
@class SASession;

// @brief:
// The SAParser class acts contains one static function that parses a
// network-received dictionary into an Ad
// @param - adDict: A NSDictionary parser by ObjC from a JSON
// @param - placementId - the placement id of the ad that's been requested
// @param - parse - a callback that actually returns the ad
@interface SAAdParser : NSObject

/**
 *  Parse ad data from jetwork
 *
 *  @param jsonData    a NSData object with json contents
 *  @param placementId the placement Id
 *  @param session     the current session
 *
 *  @return either an SAAd object or nil
 */
- (SAAd*) parseInitialAdFromNetwork:(NSString*)jsonString
                    withPlacementId:(NSInteger)placementId
                         andSession:(SASession*)session;

@end
