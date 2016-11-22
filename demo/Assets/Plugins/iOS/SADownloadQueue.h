//
//  SADownloadQueue.h
//  Pods
//
//  Created by Gabriel Coman on 30/09/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@class SADownloadItem;

@interface SADownloadQueue : NSObject

// queue operation methods
- (void) addToQueue:(SADownloadItem*)item;
- (void) removeFromQueue:(SADownloadItem*)item;
- (void) moveToBackOfQueue:(SADownloadItem*)item;

// item check & retrieve for queue
- (BOOL) hasItemForURL:(NSString*)url;
- (SADownloadItem*) itemForURL:(NSString*)url;
- (SADownloadItem*) getNext;

// get the queue length
- (NSInteger) getLength;

@end
