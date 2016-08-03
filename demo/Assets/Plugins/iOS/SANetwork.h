//
//  SANetwork.h
//  Pods
//
//  Created by Gabriel Coman on 05/07/2016.
//
//

#import <Foundation/Foundation.h>

// callback for iOS's own [NSURLConnection sendAsynchronousRequest:]
typedef void (^netresponse)(NSData * data, NSURLResponse * response, NSError * error);

// callback for generic success with data
typedef void (^success)(NSInteger status, NSString *payload);

// callback for generic failure with no data
typedef void (^failure)();

@interface SANetwork : NSObject

/**
 *  Send a GET request to an endpoint
 *
 *  @param endpoint base endpoint
 *  @param query    dictionary w/ query paramenters
 *  @param header   dictionary w/ header parameters
 *  @param success  success callback
 *  @param failure  failure callback
 */
- (void) sendGET:(NSString*)endpoint
       withQuery:(NSDictionary*)query
       andHeader:(NSDictionary*)header
      andSuccess:(success)success
      andFailure:(failure)failure;

/**
 *  Send a POST request to an endpoint
 *
 *  @param endpoint base endpoint
 *  @param query    dictionary w/ query parameters
 *  @param header   dictionary w/ header parameters
 *  @param body     dictionary w/ body paramters
 *  @param success  success callback
 *  @param failure  failure callback
 */
- (void) sendPOST:(NSString*)endpoint
        withQuery:(NSDictionary*)query
        andHeader:(NSDictionary*)header
          andBody:(NSDictionary*)body
       andSuccess:(success)success
       andFailure:(failure)failure;

@end
