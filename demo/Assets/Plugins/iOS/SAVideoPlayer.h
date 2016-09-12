//
//  SAVideoPlayer.h
//  Pods
//
//  Created by Gabriel Coman on 05/03/2016.
//
//

#import <UIKit/UIKit.h>
#import "SAURLClicker.h"

@class AVPlayer;
@class AVPlayerLayer;

// events
typedef NS_ENUM(NSInteger, SAVideoPlayerEvent) {
    Video_Start = 0,
    Video_1_4 = 1,
    Video_1_2 = 2,
    Video_3_4 = 3,
    Video_End = 4,
    Video_Error = 5
};

// callbacks
typedef void (^savideoplayerEventHandler)(SAVideoPlayerEvent event);
typedef void (^savideoplayerClickHandler)();

// class
@interface SAVideoPlayer : UIView

// public functions
- (void) playWithMediaURL:(NSURL *)url;
- (void) playWithMediaFile:(NSString*)file;
- (void) updateToFrame:(CGRect)frame;
- (void) destroy;
- (void) reset;
- (void) resume;
- (void) pause;
- (void) setEventHandler:(savideoplayerEventHandler)eventHandler;
- (void) setClickHandler:(savideoplayerClickHandler)clickHandler;
- (void) showSmallClickButton;
- (AVPlayer*) getPlayer;
- (AVPlayerLayer*) getPlayerLayer;

@end
