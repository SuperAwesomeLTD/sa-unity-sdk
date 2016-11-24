//
//  MoatAdEvent.h
//  MoatMobileAppKit
//
//  Created by Moat on 2/5/16.
//  Copyright © 2016 Moat. All rights reserved.
//
//  This class is simply a data object that encapsulates info relevant to a particular playback event.

#import <Foundation/Foundation.h>

typedef enum SUPMoatAdEventType : NSUInteger {
    SUPMoatAdEventComplete
    , SUPMoatAdEventStart
    , SUPMoatAdEventFirstQuartile
    , SUPMoatAdEventMidPoint
    , SUPMoatAdEventThirdQuartile
    , SUPMoatAdEventSkipped
    , SUPMoatAdEventStopped
    , SUPMoatAdEventPaused
    , SUPMoatAdEventPlaying
    , SUPMoatAdEventVolumeChange
    , SUPMoatAdEventNone
} SUPMoatAdEventType;

static NSTimeInterval const SUPMoatTimeUnavailable = NAN;
static float const SUPMoatVolumeUnavailable = NAN;

@interface SUPMoatAdEvent : NSObject

@property SUPMoatAdEventType eventType;
@property NSTimeInterval adPlayhead;
@property float adVolume;
@property (readonly) NSTimeInterval timeStamp;

- (id)initWithType:(SUPMoatAdEventType)eventType withPlayheadMillis:(NSTimeInterval)playhead;
- (id)initWithType:(SUPMoatAdEventType)eventType withPlayheadMillis:(NSTimeInterval)playhead withVolume:(float)volume;
- (NSDictionary *)asDict;
- (NSString *)eventAsString;

@end
