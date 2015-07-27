//
//  SAInterstitialView.m
//  SAMobileSDK
//
//  Created by Balázs Kiss on 12/08/14.
//  Copyright (c) 2014 SuperAwesome Ltd. All rights reserved.
//

#import "SAParentalGateOpener.h"
#import "SuperAwesome.h"

@interface SAParentalGateOpener ()

@property (nonatomic,strong) NSURL *clickURL;
@property (nonatomic,strong) SAParentalGate *gate;

@end

@implementation SAParentalGateOpener

- (instancetype)initWithUrl:(NSString *)urlString
{
    if(self = [super init]){
    	self.clickURL = [[NSURL alloc] initWithString:urlString];
    }
    return self;
}


- (BOOL)openGate
{
    if(self.gate == nil){
        self.gate = [[SAParentalGate alloc] init];
        self.gate.delegate = self;
    }
    [self.gate show];
}

#pragma mark - SAParentalGateOpenerDelegate

- (void)didGetThroughParentalGate:(SAParentalGate *)parentalGate
{    
    [[UIApplication sharedApplication] openURL:self.clickURL];
}


@end
