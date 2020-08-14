# Vibration

Native plugin for Unity for iOS and Android.
Use custom vibrations on mobile.

# Installation

Copy and Postethe entire `Vibration` folder in your Unity3D `Assets` folder.

# Use

## Initiatlisation

Initialize the plugin with this line before using vibrations:

`Vibration.Init();`

## Vibrations

**iOS and Android**

* Use `Vibration.Vibrate();` for a classic default ~400ms vibration

* Pop vibration: weak boom (For iOS: only available with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibratePop();`

* Peek vibration: strong boom (For iOS: only available on iOS with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibratePeek();`

* Nope vibration: series of three weak booms (For iOS: only available with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibrateNope();`


**Android Only**

* Custom duration in milliseconds

`Vibration.Vibrate(500);` 

* Pattern

```
long [] pattern = { 0, 1000, 1000, 1000, 1000 };
Vibration.Vibrate ( pattern, -1 );
```

* Cancel

`Vibration.Cancel();`



