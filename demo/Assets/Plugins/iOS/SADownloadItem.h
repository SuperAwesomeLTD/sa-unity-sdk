//
//  SADownloadItem.h
//  Pods
//
//  Created by Gabriel Coman on 30/09/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface SADownloadItem : NSObject

@property (nonatomic, strong) NSString *urlKey;
@property (nonatomic, strong) NSString *diskUrl;
@property (nonatomic, strong) NSString *ext;
@property (nonatomic, assign) BOOL isOnDisk;
@property (nonatomic, assign) NSInteger nrRetries;
@property (nonatomic, strong) NSMutableArray *responses;

- (void) incrementNrRetries;
- (void) clearResponses;
- (void) addResponse:(id)response;

@end
