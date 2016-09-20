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

//
// This is just a "dumb" function that sends a GET request to the
// specified URL; it's a thing wrapper for SANetwork.sendGetToEndpoint() so that
// it's fire-and-forget
// @param: the URL to make the event request to
- (void) sendEventToURL:(NSString*)url;

/**
 *  Method that sends all events for a particular key, for objects of type SATracking
 *
 *  @param key    key to send for
 */
- (void) sendAllEventsForKey:(NSString*)key;

/**
 *  Send viewable impresison for in-screen
 *
 *  @param view <#view description#>
 */
- (void) sendViewableImpressionForDisplay:(UIView*) view;
- (void) sendViewableImpressionForVideo:(UIView*) view;

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

@end
