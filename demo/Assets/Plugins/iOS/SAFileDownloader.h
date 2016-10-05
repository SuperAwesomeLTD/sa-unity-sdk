//
//  SASequentialFileDownloader.h
//  Pods
//
//  Created by Gabriel Coman on 30/09/2016.
//
//

#import <UIKit/UIKit.h>
#import "SADownloadItem.h"

// callback for generic success
typedef void (^seqDownloadResponse)(BOOL success, NSString* diskPath);

@interface SAFileDownloader : NSObject

// singleton instance (instead of init)
+ (instancetype) getInstance;

// public methods
- (NSString*) getDiskLocation:(NSString*) extension;
- (void) downloadFileFrom:(NSString*)url
            withExtension:(NSString*)ext
              andResponse:(seqDownloadResponse)response;

@end
