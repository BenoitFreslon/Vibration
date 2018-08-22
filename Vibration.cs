using UnityEngine;
using System.Collections;

public class Vibration
{

#if UNITY_IOS

    [DllImport ( "__Internal" )]
    private static extern bool _HasVibrator ();

    [DllImport ( "__Internal" )]
    private static extern void _Vibrate ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePop ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePeek ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateNope ();
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
        string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string> ( "VIBRATOR_SERVICE" );
        AndroidJavaObject systemService = context.Call<AndroidJavaObject> ( "getSystemService", Context_VIBRATOR_SERVICE );
        if ( systemService.Call<bool> ( "hasVibrator" ) ) {
            return true;
        } else
            return false;
#elif UNITY_IOS
        return _HasVibratior();
#endif
    }

    public static void Cancel ()
    {
#if UNITY_ANDROID
            vibrator.Call ( "cancel" );
#endif
    }
}