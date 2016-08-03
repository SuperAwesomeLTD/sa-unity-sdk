//
//  SAExtensions.h
//  Pods
//
//  Created by Gabriel Coman on 09/03/2016.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

////////////////////////////////////////////////////////////////////////////////
// SuperAwesome extension for NSDictionary
////////////////////////////////////////////////////////////////////////////////

@interface NSDictionary (SAExtensions)

- (void) enumerateKeysAndObjectsUsingBlock:(nullable void (^)(id _Nonnull key, id _Nonnull obj, BOOL * _Nonnull end))block
                                     atEnd:(nullable void (^)())end;

@end

////////////////////////////////////////////////////////////////////////////////
// SuperAwesome extension for NSArray
////////////////////////////////////////////////////////////////////////////////

@interface NSArray (SAExtensions)

- (nonnull NSArray*) filterBy:(nonnull NSString*) member withValue:(nonnull NSString*) value;
- (nonnull NSArray*) filterBy:(nonnull NSString*) member withBool:(BOOL)value;
- (nonnull NSArray*) removeAllButFirstElement;

@end

////////////////////////////////////////////////////////////////////////////////
// SuperAwesome extension for UIAlertController
////////////////////////////////////////////////////////////////////////////////

@interface UIAlertController (SAExtensions)

- (void)show;
- (void)show:(BOOL)animated;

@end