//
//  SAVAST2Parser.h
//  Pods
//
//  Created by Gabriel Coman on 17/12/2015.
//
//

#import <Foundation/Foundation.h>

// forward declaration
@class SAVASTAd;

typedef void (^vastParsingDone)(SAVASTAd *ad);

////////////////////////////////////////////////////////////////////////////////
// The VASTParser main protocol
////////////////////////////////////////////////////////////////////////////////

// The SAVASTParserProtocol implements two functions
@protocol SAVASTParserProtocol <NSObject>

// Called as a callback when there are valid ads to be displayed
- (void) didParseVAST:(SAVASTAd*)ad;

@end

////////////////////////////////////////////////////////////////////////////////
// The VASTParser main class
////////////////////////////////////////////////////////////////////////////////

@interface SAVASTParser : NSObject

// weak object delegate to SAVAStParserProtocol
@property (nonatomic, weak) id<SAVASTParserProtocol> delegate;

// parse the VAST URL
- (void) parseVASTURL:(NSString*)url;
- (void) parseVASTURL:(NSString *)url done:(vastParsingDone)vastParsing;

@end
