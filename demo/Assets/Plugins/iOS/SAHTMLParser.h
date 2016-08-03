//
//  SAFormatter.h
//  Pods
//
//  Copyright (c) 2015 SuperAwesome Ltd. All rights reserved.
//
//  Created by Gabriel Coman on 12/10/2015.
//
//

#import <Foundation/Foundation.h>

// forward declaration
@class SAAd;

// @brief:
// This class contains class functions that generate the HTML for
// image, rich media and tag ads that need to be rendered in a WebView
@interface SAHTMLParser : NSObject

// formats HTML text based on ad type
+ (NSString*) formatCreativeDataIntoAdHTML:(SAAd*)ad;

@end
