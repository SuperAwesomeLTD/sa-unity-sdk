//
//  SAUtils.h
//  Pods
//
//  Created by Gabriel Coman on 09/03/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

////////////////////////////////////////////////////////////////////////////////
// Different "globally" available declarations; for enums, typedefs, etc
////////////////////////////////////////////////////////////////////////////////

// enum that lists the available system sizes
typedef enum SASystemSize {
    size_phone = 0,
    size_tablet = 1
}SASystemSize;

typedef enum SAConnectionType {
    unknown = 0,
    ethernet = 1,
    wifi = 2,
    cellular_unknown = 3,
    cellular_2g = 4,
    cellular_3g = 5,
    cellular_4g = 6
}SAConnectionType;

@interface SAUtils : NSObject

////////////////////////////////////////////////////////////////////////////////
// Trully aux functions
////////////////////////////////////////////////////////////////////////////////

+ (CGRect) mapOldFrame:(CGRect)frame toNewFrame:(CGRect)oldframe;
+ (BOOL) isRect:(CGRect)target inRect:(CGRect)frame;
+ (NSInteger) randomNumberBetween:(NSInteger)min maxNumber:(NSInteger)max;
+ (NSString*) findSubstringFrom:(NSString*)source betweenStart:(NSString*)start andEnd:(NSString*)end;
+ (NSString*) generateUniqueKey;

////////////////////////////////////////////////////////////////////////////////
// System type functions
////////////////////////////////////////////////////////////////////////////////

+ (SASystemSize) getSystemSize;
+ (NSString*) getVerboseSystemDetails;
+ (NSString*) filePathInDocuments:(NSString*)fpath;

////////////////////////////////////////////////////////////////////////////////
// URL and Network request helper classes
////////////////////////////////////////////////////////////////////////////////

+ (NSString*) getUserAgent;
+ (NSInteger) getCachebuster;
+ (NSString*) formGetQueryFromDict:(NSDictionary*)dict;
+ (NSString*) encodeURI:(NSString*)stringToEncode;
+ (NSString*) encodeJSONDictionaryFromNSDictionary:(NSDictionary*)dict;
+ (NSString*) decodeHTMLEntitiesFrom:(NSString*)string;
+ (NSString*) findBaseURLFromResourceURL:(NSString*)resourceURL;
+ (BOOL) isValidURL:(NSObject*) urlObject;
+ (BOOL) isEmailValid:(NSString*) email;

////////////////////////////////////////////////////////////////////////////////
// Colors
////////////////////////////////////////////////////////////////////////////////

UIColor *UIColorFromHex (int rgbValue);
UIColor *UIColorFromRGB (NSInteger red, NSInteger green, NSInteger blue);

////////////////////////////////////////////////////////////////////////////////
// Aux network functions
////////////////////////////////////////////////////////////////////////////////

// network connectivity
+ (SAConnectionType) getNetworkConnectivity;

////////////////////////////////////////////////////////////////////////////////
// Invocation
////////////////////////////////////////////////////////////////////////////////

+ (NSValue*) invoke:(NSString*)method onTarget:(id) target, ...;
+ (NSValue*) invoke:(NSString*)method onClass:(NSString*) name, ...;


@end
