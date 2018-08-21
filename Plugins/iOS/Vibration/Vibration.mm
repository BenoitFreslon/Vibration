//
//  Vibration.mm
//  https://videogamecreation.fr
//
//  Created by Benoît Freslon on 23/03/2017.
//  Copyright © 2018 Benoît Freslon. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <AudioToolbox/AudioToolbox.h>
#import <UIKit/UIKit.h>

#import "Vibration.h"

#define USING_IPAD UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad

@interface Vibration ()

@end

@implementation Vibration



//////////////////////////////////////////

#pragma mark - Vibrate

+ (BOOL)    hasVibrator {
    return !(USING_IPAD);
}
+ (void)    vibrate {
    AudioServicesPlaySystemSound(1352);
}

+ (void)    vibratePeek {
    AudioServicesPlaySystemSound(1519); // Actuate `Peek` feedback (weak boom)
}
+ (void)    vibratePop {
    AudioServicesPlaySystemSound(1520); // Actuate `Pop` feedback (strong boom)
}
+ (void)    vibrateNope {
    AudioServicesPlaySystemSound(1521); // Actuate `Nope` feedback (series of three weak booms)
}

@end
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#pragma mark - "C"

extern "C" {
    
    //////////////////////////////////////////
    // Vibrate
    
    bool    _HasVibrator () {
        return [Vibration hasVibrator];
    }
 
    void    _Vibrate () {
        [Vibration hasVibrator];
    }
    
    void    _VibratePeek () {
        [Vibration vibratePeek];
    }
    void    _VibratePop () {
        [Vibration vibratePop];
    }
    void    _VibrateNope () {
        [Vibration vibrateNope];
    }
    
}

