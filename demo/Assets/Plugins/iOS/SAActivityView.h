//
//  SAActivityView.h
//  Pods
//
//  Created by Gabriel Coman on 07/07/2016.
//
//

#import <UIKit/UIKit.h>

@interface SAActivityView : NSObject

// singleton accessor
+ (instancetype) sharedManager;

// class methods
- (void) showActivityView;
- (void) hideActivityView;

@end
