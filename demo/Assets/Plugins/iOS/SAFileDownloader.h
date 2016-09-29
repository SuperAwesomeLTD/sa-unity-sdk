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
typedef void (^downloadResponse)(BOOL success);

//
// class that deals with downloading (and cleaning-up) files
@interface SAFileDownloader : NSObject

/**
 *
 * @param extension     the extension for the file
 * @return a new disk location
 */
+ (NSString*) getDiskLocation:(NSString*) extension;

/**
 *  Downloa a file from a remote URL to a predefined file path
 *
 *  @param url      the remote URL
 *  @param fpath    the predefine lication
 *  @param response download response
 */
- (void) downloadFileFrom:(NSString*)url
                       to:(NSString*)fpath
              withResponse:(downloadResponse)response;

@end
