//
//  SACapper.h
//  Pods
//
//  Created by Gabriel Coman on 16/02/2016.
//
//

#import <Foundation/Foundation.h>

typedef void (^didFindDAUId)(NSUInteger dauId);

@interface SACapper : NSObject

//
// static method to enable Capping
// Through it's listener interface it returns a dauID
// The dauID can be non-0 -> in which case it's valid
// or it can be 0 -> in which case it's not valid (user does not have tracking enabled or
// gms enabled)
- (void) enableCapping:(didFindDAUId) callback;

@end
