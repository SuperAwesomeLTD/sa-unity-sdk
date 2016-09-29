//
//  SASession.h
//  Pods
//
//  Created by Gabriel Coman on 15/07/2016.
//
//

#import <UIKit/UIKit.h>

typedef NS_ENUM(NSInteger, SAConfiguration) {
    PRODUCTION = 0,
    STAGING = 1
};

@interface SASession : NSObject

// setters
// config
- (void) setConfiguration:(SAConfiguration) configuration;
- (void) setConfigurationProduction;
- (void) setConfigurationStaging;
// test mode
- (void) enableTestMode;
- (void) disableTestMode;
- (void) setTestMode:(BOOL) testEnabled;
// others
- (void) setDauId:(NSInteger)dauId;
- (void) setVersion:(NSString *)version;

// getters
- (NSString*) getBaseUrl;
- (BOOL)      getTestMode;
- (NSInteger) getDauId;
- (NSString*) getVersion;
- (SAConfiguration) getConfiguration;
- (NSString*) getBundleId;
- (NSString*) getAppName;
- (NSString*) getLang;
- (NSString*) getDevice;
- (NSInteger) getConnectivityType;
- (NSInteger) getCachebuster;
- (NSString*) getUserAgent;

@end
