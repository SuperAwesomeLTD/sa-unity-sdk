//
//  SAFileDownloader.h
//  Pods
//
//  Created by Gabriel Coman on 18/04/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "SANetwork.h"

// callback for generic success
typedef void (^downloadFinish)();

//
// class that deals with downloading (and cleaning-up) files
@interface SAFileDownloader : NSObject

//
// singleton instance (instead of init)
+ (SAFileDownloader*) getInstance;

/**
 *  Function that gets a new file location on disk
 *
 *  @return a new file location
 */
- (NSString*) getDiskLocation;

/**
 *  Downloa a file from a remote URL to a predefined file path
 *
 *  @param url     the remote URL
 *  @param fpath   the predefine lication
 *  @param success success callback
 *  @param failure failure callback
 */
- (void) downloadFileFrom:(NSString*)url
                       to:(NSString*)fpath
              withSuccess:(downloadFinish)success
                orFailure:(failure)failure;

@end
