//
//  SAEvents.h
//  Pods
//
//  Created by Gabriel Coman on 26/02/2016.
//
//

#import <UIKit/UIKit.h>

//
// forward declarations
@class AVPlayer;
@class AVPlayerLayer;
//
// SAEvents is a class that contains a multitude of functions used to send
// messages to the server in case some event gets triggered regarding an Ad
// such as viewable impression, ad rating, etc
@interface SAEvents : NSObject

//
// This is just a "dumb" function that sends a GET request to the
// specified URL; it's a thing wrapper for SANetwork.sendGetToEndpoint() so that
// it's fire-and-forget
// @param: the URL to make the event request to
+ (void) sendEventToURL:(NSString*)url;

/**
 *  Send custom event
 *
 *  @param baseUrl     baseUrl of the request
 *  @param placementId placementId integer
 *  @param lineItem    lineItemId integer
 *  @param creative    creative id
 *  @param event       custom event
 */
+ (void) sendCustomEvent:(NSString*) baseUrl
           withPlacement:(NSInteger) placementId
            withLineItem:(NSInteger) lineItem
             andCreative:(NSInteger) creative
                andEvent:(NSString*) event;

@end
