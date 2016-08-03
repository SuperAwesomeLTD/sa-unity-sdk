//
//  SAProtocols.h
//  Pods
//
//  Created by Gabriel Coman on 14/02/2016.
//
//

#import <Foundation/Foundation.h>

// useful imports
@class SAAd;

////////////////////////////////////////////////////////////////////////////////

// @brief:
// This protocol informs the user about different events in the lifecylce
// of a normal Ad;
// It has to be implemented as a delegate object by any child of
// SAView, meaning any Ad type is valid
@protocol SAAdProtocol <NSObject>

@optional

// this function is called when the ad is shown on the screen
- (void) adWasShown:(NSInteger)placementId;

// this function is called when the ad failed to show
- (void) adFailedToShow:(NSInteger)placementId;

// this function is called when an ad is closed;
// only applies to fullscreen ads like interstitials and fullscreen videos
- (void) adWasClosed:(NSInteger)placementId;

// this function is called when an ad is clicked
- (void) adWasClicked:(NSInteger)placementId;

// this is called in case of incorrect format - e.g. user calls a video
// placement for an interstitial, etc
- (void) adHasIncorrectPlacement:(NSInteger)placementId;

@end

////////////////////////////////////////////////////////////////////////////////

// @brief:
// A custom protocol that defines functions that respond to parental gate
// actions
@protocol SAParentalGateProtocol <NSObject>

// all functions are optional
@optional

// this function is called when a parental gate pop-up "cancel" button is pressed
- (void) parentalGateWasCanceled:(NSInteger)placementId;

// this function is called when a parental gate pop-up "continue" button is
// pressed and the parental gate failed (because the numbers weren't OK)
- (void) parentalGateWasFailed:(NSInteger)placementId;

// this function is called when a parental gate pop-up "continue" button is
// pressed and the parental gate succedded
- (void) parentalGateWasSucceded:(NSInteger)placementId;

@end

////////////////////////////////////////////////////////////////////////////////

// @brief:
// This is the SAVideoProtocol implementation, that defines a series of
// functions that might be of interest to SDK users who want to have specific
// actions in case of video events
@protocol SAVideoAdProtocol <NSObject>

// all functions are optional
@optional

// fired when an ad has started
- (void) adStarted:(NSInteger)placementId;

// fired when a video ad has started
- (void) videoStarted:(NSInteger)placementId;

// fired when a video ad has reached 1/4 of total duration
- (void) videoReachedFirstQuartile:(NSInteger)placementId;

// fired when a video ad has reached 1/2 of total duration
- (void) videoReachedMidpoint:(NSInteger)placementId;

// fired when a video ad has reached 3/4 of total duration
- (void) videoReachedThirdQuartile:(NSInteger)placementId;

// fired when a video ad has ended
- (void) videoEnded:(NSInteger)placementId;

// fired when an ad has ended
- (void) adEnded:(NSInteger)placementId;

// fired when all ads have ended
- (void) allAdsEnded:(NSInteger)placementId;

@end

////////////////////////////////////////////////////////////////////////////////

@protocol SAViewProtocol <NSObject>

// sets the ad for a SABannerAd, SAInterstitialAd, etc type class
- (void) setAd:(SAAd*)ad;

// return an Ad
- (SAAd*) getAd;

// show padlock
- (BOOL) shouldShowPadlock;

// play the ad
- (void) play;

// close the ad
- (void) close;

// second pass at trying to go to URL
- (void) advanceToClick;

// resize view or view controller
- (void) resizeToFrame:(CGRect)frame;

@end
