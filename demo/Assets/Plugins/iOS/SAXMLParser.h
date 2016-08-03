//
//  SAXMLParser.h
//  SAXMLParser
//
//  Created by Gabriel Coman on 18/04/2016.
//  Copyright © 2016 Gabriel Coman. All rights reserved.
//

#import <Foundation/Foundation.h>

//
// The SAXMLElement object
@interface SAXMLElement : NSObject

// members
@property (nonatomic, strong) NSString *name;
@property (nonatomic, strong) NSString *value;
@property (nonatomic, strong) NSDictionary *attributes;
@property (nonatomic, strong) NSMutableArray *children;
@property (nonatomic, weak) SAXMLElement *parent;

// functions
- (NSString*) getName;
- (NSString*) getValue;
- (NSString*) getAttribute:(NSString*)name;

@end

//
// main parser definition
@interface SAXMLParser : NSObject

// the error - in case of any
@property (nonatomic, strong) NSError *errorResult;

/**
 *  Function that perses a bunch of XML data
 *
 *  @param xml the XML data object
 */
- (SAXMLElement*) parseXMLData:(NSData*)xml;

/**
 *  Function that perses a bunch of XML strings
 *
 *  @param xml the xml string
 */
- (SAXMLElement*) parseXMLString:(NSString*)xml;

/**
 *  Get the error
 *
 *  @return Error object
 */
- (NSError*)getError;

/**
 *  Function that prints all the level
 */
- (void) printLevels;

@end

// typedef for an iterator block
typedef void (^SAXMLIterateBlock)(SAXMLElement *element);

// static functions over the parser
@interface SAXMLParser (SAStaticFunctions)

//
// @brief: function that returns an array of found elements that correspond to
// the name given for the search, given that the search starts from the current
// element and checks all siblings and children
+ (NSMutableArray*) searchSiblingsAndChildrenOf:(SAXMLElement*)element
                                        forName:(NSString*)name;

//
// @brief: shorthand version of a function that returns the first intance of
// a TBXMLElement wrapped as a NSValue
+ (SAXMLElement*) findFirstIntanceInSiblingsAndChildrenOf:(SAXMLElement*)element
                                                  forName:(NSString*)name;

//
// @brief: doing the same thing as the function above, only the result is passed down
// as an interation block
+ (void) searchSiblingsAndChildrenOf:(SAXMLElement*)element
                             forName:(NSString*)name
                         andInterate:(SAXMLIterateBlock)block;

//
// @brief: a function that returns a boolean if at least one element given the name
// is found in all siblings and children of the current element
+ (BOOL) checkSiblingsAndChildrenOf:(SAXMLElement*)element
                            forName:(NSString*)name;

@end
