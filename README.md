# Vibration
Native plugin for Unity for iOS and Android.
Use custom vibrations on mobile.

**iOS and Android**

* Use `Vibration.Vibrate();` for a default 500ms vibration

**iOS Only**

* Pop vibration: weak boom (only available with the haptic engine: iPhone 6s minimum)

`Vibration.VibratePop();`

* Peek vibration: strong boom (only available with the haptic engine: iPhone 6s minimum)

`Vibration.VibratePeek();`

* Nope vibration: series of three weak booms (only available with the haptic engine: iPhone 6s minimum)

`Vibration.VibrateNope();`


**Android Only**

* Custom duration in milliseconds

`Vibration.Vibrate(500);` 

* Pattern

```
long [] pattern = { 0, 1000, 1000, 1000, 1000 };
Vibration.Vibrate ( pattern, 0 );
```

* Cancel

`Vibration.Cancel();`



