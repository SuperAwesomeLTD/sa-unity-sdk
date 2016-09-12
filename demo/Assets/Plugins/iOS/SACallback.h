//
//  SACallback.h
//  Pods
//
//  Created by Gabriel Coman on 09/09/2016.
//
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, SAEvent) {
    adLoaded = 0,
    adFailedToLoad = 1,
    adShown = 2,
    adFailedToShow = 3,
    adClicked = 4,
    adClosed = 5
};

typedef void (^sacallback)(NSInteger placementId, SAEvent event);