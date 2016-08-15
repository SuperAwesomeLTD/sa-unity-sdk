//
//  SANetwork.h
//  Pods
//
//  Created by Gabriel Coman on 05/07/2016.
//
//

#import <Foundation/Foundation.h>

// callback for generic success with data
typedef void (^response)(NSInteger status, NSString *payload, BOOL success);

@interface SANetwork : NSObject

/**
 *  Send a GET request to an endpoint
 *
 *  @param endpoint base endpoint
 *  @param query    dictionary w/ query paramenters
 *  @param header   dictionary w/ header parameters
 *  @param response response callback
 */
- (void) sendGET:(NSString*)endpoint
       withQuery:(NSDictionary*)query
       andHeader:(NSDictionary*)header
    withResponse:(response)response;

/**
 *  Send a POST request to an endpoint
 *
 *  @param endpoint base endpoint
 *  @param query    dictionary w/ query parameters
 *  @param header   dictionary w/ header parameters
 *  @param body     dictionary w/ body paramters
 *  @param response response callback
 */
- (void) sendPOST:(NSString*)endpoint
        withQuery:(NSDictionary*)query
        andHeader:(NSDictionary*)header
          andBody:(NSDictionary*)body
     withResponse:(response)response;

@end
