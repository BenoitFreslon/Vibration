//
//  Vibration.h
//  https://videogamecreation.fr
//
//  Created by Benoît Freslon on 23/03/2017.
//  Copyright © 2018 Benoît Freslon. All rights reserved.
//
#import <Foundation/Foundation.h>

@interface Vibration : NSObject

//////////////////////////////////////////

#pragma mark - Vibrate

+ (BOOL)    hasVibrator;
+ (void)    vibrate;
+ (void)    vibratePeek;
+ (void)    vibratePop;
+ (void)    vibrateNope;

//////////////////////////////////////////


@end

