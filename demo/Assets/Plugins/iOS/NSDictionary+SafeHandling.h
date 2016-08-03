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
static id _Nullable nullSafe(id object) {
    return object ?: [NSNull null];
}

@interface NSDictionary (SafeHandling)

/**
 *  Function that safely gets an object's value (or returns nil - which is safe
 *  when trying to apply functions to it)
 *
 *  @param aKey the key
 *
 *  @return the return value or nil
 */
-  (id) safeObjectForKey:(id)aKey;

@end
