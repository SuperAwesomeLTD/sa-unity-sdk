//
//  SAEvents.h
//  Pods
//
//  Created by Gabriel Coman on 26/02/2016.
//
//

#import <UIKit/UIKit.h>

// typical event response (used mostly for testing purposes atm)
typedef void (^saEventResponse)(BOOL success, NSInteger status);

//
// forward declarations
@class SAAd;
@class AVPlayer;
@class AVPlayerLayer;

//
// SAEvents is a class that contains a multitude of functions used to send
// messages to the server in case some event gets triggered regarding an Ad
// such as viewable impression, ad rating, etc
@interface SAEvents : NSObject

// custom init
//
- (void) setAd:(SAAd*)ad;

/**
 * 
 * Send an event to an URL
 *
 * @param url endpoint to send the event to
 */
- (void) sendEventToURL:(NSString*)url;

/**
 *
 * Send an event to an URL
 *
 * @param url endpoint to send the event to
 * @param response callback from the event
 */
- (void) sendEventToURL:(NSString*)url withResponse:(saEventResponse)response;

/**
 *  Method that sends all events for a particular key, for objects of type SATracking
 *
 *  @param key    key to send for
 */
- (void) sendAllEventsForKey:(NSString*)key withResponse:(saEventResponse)response;
- (void) sendAllEventsForKey:(NSString*)key;

/**
 *  Send viewable impresison event launchers
 *
 *  @param view     the view that will need to be measured
 *  @param ticks    number of ticks to test against before a viewable 
                    impression can be launched
 */
- (void) sendViewableImpressionForView:(UIView*)view andTicks:(NSInteger)maxTicks withResponse:(saEventResponse)response;
- (void) sendViewableImpressionForDisplay:(UIView*)view;
- (void) sendViewableImpressionForVideo:(UIView*)view;

/**
 *  Method that returns a MOAT string
 *
 *  @param webplayer a SAWebPlayer object
 *
 *  @return moat web
 */
- (NSString*) moatEventForWebPlayer:(id)webplayer;

/**
 *  Method that starts MOAT tracking for video
 *
 *  @param player the video player
 *  @param layer  the video layer
 *  @param view   the containing view
 */
- (void) moatEventForVideoPlayer:(AVPlayer*)player withLayer:(AVPlayerLayer*)layer andView:(UIView*)view;

/**
 *  Close method
 */
- (void) close;

/**
 *  Disable moat limiting
 */
- (void) disableMoatLimiting;

@end
