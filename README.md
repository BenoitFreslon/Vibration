# Vibration
Native plugin for Unity for iOS and Android.
Use custom vibrations on mobile.

**iOS and Android**

* Use `Vibration.Vibrate();` for a classic default ~400ms vibration

* Pop vibration: weak boom (For iOS: only available with the haptic engine. iPhone 6s minimum)

`Vibration.VibratePop();`

* Peek vibration: strong boom (For iOS: only available on iOS with the haptic engine. iPhone 6s minimum)

`Vibration.VibratePeek();`

* Nope vibration: series of three weak booms (For iOS: only available with the haptic engine. iPhone 6s minimum)

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



