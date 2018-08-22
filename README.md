# Vibration
Native plugin for Unity for iOS and Android.
Use custom vibrations on mobile.

**iOS and Android**

* Use `Vibration.Vibrate();` for a 300ms vibration

**iOS Only**

* Pop vibration (iPhone 6 minimum)

`Vibration.VibratePop();`

* Peek vibration (iPhone 6 minimum)

`Vibration.VibratePeek();`

* Nope vibration (iPhone 6 minimum)

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



