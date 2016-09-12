//
//  SAVAST2Parser.h
//  Pods
//
//  Created by Gabriel Coman on 17/12/2015.
//
//

#import <Foundation/Foundation.h>

// forward declaration
@class SAAd;

// callback
typedef void (^vastParsingDone)(SAAd *ad);

////////////////////////////////////////////////////////////////////////////////
// The VASTParser main class
////////////////////////////////////////////////////////////////////////////////

@interface SAVASTParser : NSObject

// functions
- (void) parseVASTURL:(NSString *)url done:(vastParsingDone)vastParsing;

@end
