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

- (void) addToQueue:(SADownloadItem*)item;
- (void) removeFromQueue:(SADownloadItem*)item;
- (BOOL) hasItemForURL:(NSString*)url;
- (SADownloadItem*) itemForURL:(NSString*)url;
- (NSInteger) getLength;
- (SADownloadItem*) getNext;

@end
