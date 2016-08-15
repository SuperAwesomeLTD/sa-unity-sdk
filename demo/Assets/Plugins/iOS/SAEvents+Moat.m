//
//  SAEvents+Moat.m
//  Pods
//
//  Created by Gabriel Coman on 01/06/2016.
//
//

#import "SAEvents+Moat.h"
#if defined(__has_include)
#if __has_include("SUPMoatMobileAppKit.h")
    #import "SUPMoatMobileAppKit.h"
    #define HAS_MOAT true
#else 
    #define HAS_MOAT false
#endif
#endif
#import "SAUtils.h"

#define MOAT_SERVER @"https://z.moatads.com"
#define MOAT_URL @"moatad.js"
#define MOAT_DISPLAY_PARTNER_CODE @"superawesomeinappdisplay731223424656"
#define MOAT_VIDEO_PARTNER_CODE @"superawesomeinappvideo467548716573"

@implementation SAEvents (Moat)

+ (NSString*) sendDisplayMoatEvent:(UIWebView*)webView andAdDictionary:(NSDictionary*)adDict{
    
    // make only 1 in 5 moat events OK
    NSInteger rand = [SAUtils randomNumberBetween:0 maxNumber:100];
    if (rand > 20) {
        NSLog(@"[AA:: Info] Moat Display Event not triggered this time");
        return @"";
    }
    
#if HAS_MOAT
    NSLog(@"MOAT can be triggered");
    // go ahead
    [SUPMoatBootstrap injectDelegateWrapper:webView];
    
    NSMutableString *moatQuery = [[NSMutableString alloc] init];
    [moatQuery appendFormat:@"moatClientLevel1=%@", [adDict objectForKey:@"advertiser"]];
    [moatQuery appendFormat:@"&moatClientLevel2=%@", [adDict objectForKey:@"campaign"]];
    [moatQuery appendFormat:@"&moatClientLevel3=%@", [adDict objectForKey:@"line_item"]];
    [moatQuery appendFormat:@"&moatClientLevel4=%@", [adDict objectForKey:@"creative"]];
    [moatQuery appendFormat:@"&moatClientSlicer1=%@", [adDict objectForKey:@"app"]];
    [moatQuery appendFormat:@"&moatClientSlicer2=%@", [adDict objectForKey:@"placement"]];
    [moatQuery appendFormat:@"&moatClientSlicer3=%@", [adDict objectForKey:@"publisher"]];
    
    return [NSString stringWithFormat:@"<script src=\"%@/%@/%@?%@\" type=\"text/javascript\"></script>", MOAT_SERVER, MOAT_DISPLAY_PARTNER_CODE, MOAT_URL, moatQuery];
#else
    NSLog(@"No moat present!");
    return @"";
#endif
}

+ (void) sendVideoMoatEvent:(AVPlayer*)player andLayer:(AVPlayerLayer*)layer andView:(UIView*)adView andAdDictionary:(NSDictionary*)adDict {
    
    // make only 1 in 5 moat events OK
    NSInteger rand = [SAUtils randomNumberBetween:0 maxNumber:100];
    if (rand > 20) {
        NSLog(@"[AA:: Info] Moat Video Event not triggered this time");
        return;
    }
    
#if HAS_MOAT
    
    NSDictionary *moatDictionary = @{
                                     @"level1": [adDict objectForKey:@"advertiser"],
                                     @"level2": [adDict objectForKey:@"campaign"],
                                     @"level3": [adDict objectForKey:@"line_item"],
                                     @"level4": [adDict objectForKey:@"creative"],
                                     @"slicer1": [adDict objectForKey:@"app"],
                                     @"slicer2": [adDict objectForKey:@"placement"],
                                     @"slicer3": [adDict objectForKey:@"publisher"]
                                     };
    
    // go ahead
    SUPMoatVideoTracker *tracker = [SUPMoatVideoTracker trackerWithPartnerCode:MOAT_VIDEO_PARTNER_CODE];
    [tracker trackVideoAd:moatDictionary
       usingAVMoviePlayer:player
                withLayer:layer
       withContainingView:adView];
    
    NSLog(@"[AA :: Info] Sending Video Event to Moat");
#endif
}

@end
