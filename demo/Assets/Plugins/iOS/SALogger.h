//
//  KWSLogger.h
//  Pods
//
//  Created by Gabriel Coman on 07/07/2016.
//
//

#import <UIKit/UIKit.h>

@interface SALogger : NSObject

+ (void) log:(NSString*)message;
+ (void) err:(NSString*)message;

@end
