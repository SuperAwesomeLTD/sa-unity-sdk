//
//  SASession.h
//  Pods
//
//  Created by Gabriel Coman on 15/07/2016.
//
//

#import <UIKit/UIKit.h>

@interface SASession : NSObject

// singleton instance (instead of init)
+ (instancetype) getInstance;

// setters
- (void) setConfigurationProduction;
- (void) setConfigurationStaging;
- (void) setTestEnabled;
- (void) setTestDisabled;
- (void) setTest:(BOOL) testEnabled;
- (void) setDauId:(NSInteger)dauId;
- (void) setVersion:(NSString *)version;

// getters
- (NSString*) getBaseUrl;
- (BOOL) isTestEnabled;
- (NSInteger) getDauId;
- (NSString*) getVersion;

@end
