# Vibration

Native **free** plugin for Unity for iOS and Android.
Use custom vibrations on mobile.

If you like this free plugin, that's be cool if you can buy me a coffee 😀☕️
Send tips to https://paypal.me/UnityVibrationPlugin

# Installation

Copy and paste the entire `Vibration` folder in your Unity3D `Assets` folder or download and import the `Vibration.unitypackage` file.

# Use

## Initiatlisation

Initialize the plugin with this line before using vibrations:

`Vibration.Init();`

## Vibrations

### iOS and Android

#### Default vibration

Use `Vibration.Vibrate();` for a classic default ~400ms vibration

#### Pop vibration

Pop vibration: weak boom (For iOS: only available with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibratePop();`

#### Peek Vibration

Peek vibration: strong boom (For iOS: only available on iOS with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibratePeek();`

#### Nope Vibration

Nope vibration: series of three weak booms (For iOS: only available with the haptic engine. iPhone 6s minimum or Android)

`Vibration.VibrateNope();`

---
## Android Only

#### Custom duration in milliseconds

`Vibration.Vibrate(500);` 

#### Pattern

```
long [] pattern = { 0, 1000, 1000, 1000, 1000 };
Vibration.Vibrate ( pattern, -1 );
```

#### Cancel

`Vibration.Cancel();`

---
## IOS only
vibration using haptic engine

> Haptic Engine


`Vibration.VibrateIOS(ImpactFeedbackStyle.Light);`

`Vibration.VibrateIOS(ImpactFeedbackStyle.Medium);`

`Vibration.VibrateIOS(ImpactFeedbackStyle.Heavy);`

`Vibration.VibrateIOS(ImpactFeedbackStyle.Rigid);`

`Vibration.VibrateIOS(ImpactFeedbackStyle.Soft);`

`Vibration.VibrateIOS(NotificationFeedbackStyle.Error);`

`Vibration.VibrateIOS(NotificationFeedbackStyle.Success);`

`Vibration.VibrateIOS(NotificationFeedbackStyle.Warning);`

`Vibration.VibrateIOS_SelectionChanged();`
```