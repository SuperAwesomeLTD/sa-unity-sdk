//
//  SADownloadItem.h
//  Pods
//
//  Created by Gabriel Coman on 30/09/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

#define SA_KEY_PREFIX @"sasdkkey_"


@interface SADownloadItem : NSObject

@property (nonatomic, strong) NSString *urlKey;
@property (nonatomic, strong) NSString *diskName;
@property (nonatomic, strong) NSString *diskUrl;
@property (nonatomic, strong) NSString *key;
@property (nonatomic, assign) BOOL isOnDisk;
@property (nonatomic, assign) NSInteger nrRetries;
@property (nonatomic, strong) NSMutableArray *responses;

// custom init
- (id) initWithUrl:(NSString*) url;
- (id) initWithUrl:(NSString *)url andInitialResponse:(id)firstResponse;

// response & retry methods
- (void) incrementNrRetries;
- (BOOL) hasRetriesRemaining;
- (void) clearResponses;
- (void) addResponse:(id)response;

// aux disk methods
- (NSString*) getNewDiskName:(NSString*) url;
- (NSString*) getDocumentsDirectory;
- (NSString*) getKeyFromDiskName:(NSString*)name;

// item validity method
- (BOOL) isValid;

@end
