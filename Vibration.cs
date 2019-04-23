using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public class Vibration
{

#if UNITY_IOS

    [DllImport ( "__Internal" )]
    public static extern bool _HasVibrator ();

    [DllImport ( "__Internal" )]
    public static extern void _Vibrate ();

    [DllImport ( "__Internal" )]
    public static extern void _VibratePop ();

    [DllImport ( "__Internal" )]
    public static extern void _VibratePeek ();

    [DllImport ( "__Internal" )]
    public static extern void _VibrateNope ();
#endif

#if UNITY_ANDROID
	public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    public static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject> ( "getApplicationContext" );
#endif

    public static void Vibrate ()
    {
#if UNITY_ANDROID
        vibrator.Call ( "vibrate" );
#elif UNITY_IOS
        _Vibrate();
#endif
    }


    public static void Vibrate ( long milliseconds )
    {
#if UNITY_ANDROID
        vibrator.Call ( "vibrate", milliseconds );
#elif UNITY_IOS
        _Vibrate();
#endif
    }

    public static void Vibrate ( long[] pattern, int repeat )
    {
#if UNITY_ANDROID
        vibrator.Call ( "vibrate", pattern, repeat );
#elif UNITY_IOS
        _Vibrate();
#endif
    }

    public static bool HasVibrator ()
    {
#if UNITY_ANDROID 
        if (vibrator.Call<bool> ( "hasVibrator" ) ) {
            return true;
        } else
            return false;
#elif UNITY_IOS 
        return _HasVibrator();
#endif
    }

    public static void Cancel ()
    {
#if UNITY_ANDROID
            vibrator.Call ( "cancel" );
#endif
    }

#if UNITY_IOS
    public static void VibratePop()
    {
        _VibratePop();
    }
    public static void VibratePeek ()
    {
        _VibratePeek();
    }

    public static void VibrateNope ()
    {
        _VibrateNope();
    }
#endif
}
