//
//  NSDictionary+SafeHandling.h
//  Pods
//
//  Created by Gabriel Coman on 31/05/2016.
//
//

#import <Foundation/Foundation.h>

/**
 *  This function makes sure the data you enter in a dictionary object is not
 *  ever going to be NULL or nil; If it's not initialized, a NSNull instance
 *  will be delivered
 *
 *  @param object the object in question
 *
 *  @return either the object or NSNull
 */
__attribute__((unused)) static _Nullable id nullSafe(_Nullable id object) {
    return object ?: [NSNull null];
}

@interface NSDictionary (SafeHandling)

- (id _Nullable) safeObjectForKey:(id _Nullable)aKey;
- (id _Nullable) safeObjectForKey:(id _Nullable)aKey orDefault:(id _Nullable)def;

- (NSInteger) safeIntForKey:(id _Nullable)aKey;
- (NSInteger) safeIntForKey:(id _Nullable)aKey orDefault:(NSInteger) def;

- (BOOL) safeBoolForKey:(id _Nullable)aKey;
- (BOOL) safeBoolForKey:(id _Nullable)aKey orDefault:(BOOL) def;

- (float) safeFloatForKey:(id _Nullable)aKey;
- (float) safeFloatForKey:(id _Nullable)aKey orDefault:(float)def;

- (double) safeDoubleForKey:(id _Nullable)aKey;
- (double) safeDoubleForKey:(id _Nullable)aKey orDefault:(double)def;

- ( NSString* _Nullable) safeStringForKey:(id _Nullable)aKey;
- ( NSString* _Nullable) safeStringForKey:(id _Nullable)aKey orDefault:(NSString* _Nullable)def;

- (NSDictionary* _Nullable) safeDictionaryForKey:(id _Nullable)aKey;
- (NSDictionary* _Nullable) safeDictionaryForKey:(id _Nullable)aKey orDefault:(NSDictionary* _Nullable)def;

- (NSArray* _Nullable) safeArrayForKey:(id _Nullable)aKey;
- (NSArray* _Nullable) safeArrayForKey:(id _Nullable)aKey orDefault:(NSArray* _Nullable)def;


@end
