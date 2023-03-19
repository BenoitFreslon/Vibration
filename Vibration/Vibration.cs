////////////////////////////////////////////////////////////////////////////////
//
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

#if UNITY_IOS
using System.Collections;
using System.Runtime.InteropServices;
#endif

public static class Vibration
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

    [DllImport("__Internal")]
    private static extern void _impactOccurred(string style);

    [DllImport("__Internal")]
    private static extern void _notificationOccurred(string style);

    [DllImport("__Internal")]
    private static extern void _selectionChanged();
#endif

#if UNITY_ANDROID
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
    public static AndroidJavaObject context;

    public static AndroidJavaClass vibrationEffect;


#endif

    private static bool initialized = false;
    public static void Init ()
    {
        if ( initialized ) return;

#if UNITY_ANDROID

        if ( Application.isMobilePlatform ) {

            unityPlayer = new AndroidJavaClass ( "com.unity3d.player.UnityPlayer" );
            currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ( "currentActivity" );
            vibrator = currentActivity.Call<AndroidJavaObject> ( "getSystemService", "vibrator" );
            context = currentActivity.Call<AndroidJavaObject> ( "getApplicationContext" );

            if ( AndroidVersion >= 26 ) {
                vibrationEffect = new AndroidJavaClass ( "android.os.VibrationEffect" );
            }

        }
#endif

        initialized = true;
    }

#if UNITY_IOS
    public static void VibrateIOS(ImpactFeedbackStyle style)
    {
        _impactOccurred(style.ToString());
    }

    public static void VibrateIOS(NotificationFeedbackStyle style)
    {
        _notificationOccurred(style.ToString());
    }

    public static void VibrateIOS_SelectionChanged()
    {
        _selectionChanged();
    }    
#endif



    ///<summary>
    /// Tiny pop vibration
    ///</summary>
    public static void VibratePop ()
    {
        if ( Application.isMobilePlatform ) {
#if UNITY_IOS
        _VibratePop ();
#elif UNITY_ANDROID
            VibrateAndroid ( 50 );
#endif
        }
    }
    ///<summary>
    /// Small peek vibration
    ///</summary>
    public static void VibratePeek ()
    {
        if ( Application.isMobilePlatform ) {
#if UNITY_IOS
        _VibratePeek ();
#elif UNITY_ANDROID
            VibrateAndroid ( 100 );
#endif
        }
    }
    ///<summary>
    /// 3 small vibrations
    ///</summary>
    public static void VibrateNope ()
    {
        if ( Application.isMobilePlatform ) {
#if UNITY_IOS
        _VibrateNope ();
#elif UNITY_ANDROID
            long[] pattern = { 0, 50, 50, 50 };
            VibrateAndroid ( pattern, -1 );
#endif
        }
    }


#if UNITY_ANDROID
    ///<summary>
    /// Only on Android
    /// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
    ///</summary>
    public static void VibrateAndroid ( long milliseconds )
    {

        if ( Application.isMobilePlatform ) {
            if ( AndroidVersion >= 26 ) {
                AndroidJavaObject createOneShot = vibrationEffect.CallStatic<AndroidJavaObject> ( "createOneShot", milliseconds, -1 );
                vibrator.Call ( "vibrate", createOneShot );

            } else {
                vibrator.Call ( "vibrate", milliseconds );
            }
        }
    }

    ///<summary>
    /// Only on Android
    /// https://proandroiddev.com/using-vibrate-in-android-b0e3ef5d5e07
    ///</summary>
    public static void VibrateAndroid ( long[] pattern, int repeat )
    {
        if ( Application.isMobilePlatform ) {
            if ( AndroidVersion >= 26 ) {
                long[] amplitudes;
                AndroidJavaObject createWaveform = vibrationEffect.CallStatic<AndroidJavaObject> ( "createWaveform", pattern, repeat );
                vibrator.Call ( "vibrate", createWaveform );

            } else {
                vibrator.Call ( "vibrate", pattern, repeat );
            }
        }
    }
#endif
    
    ///<summary>
    ///Only on Android
    ///</summary>
    public static void CancelAndroid ()
    {
        if ( Application.isMobilePlatform ) {
#if UNITY_ANDROID
            vibrator.Call ( "cancel" );
#endif
        }
    }

    public static bool HasVibrator ()
    {
        if ( Application.isMobilePlatform ) {

#if UNITY_ANDROID

            AndroidJavaClass contextClass = new AndroidJavaClass ( "android.content.Context" );
            string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string> ( "VIBRATOR_SERVICE" );
            AndroidJavaObject systemService = context.Call<AndroidJavaObject> ( "getSystemService", Context_VIBRATOR_SERVICE );
            if ( systemService.Call<bool> ( "hasVibrator" ) ) {
                return true;
            } else {
                return false;
            }

#elif UNITY_IOS
        return _HasVibrator ();
#else
        return false;
#endif
        } else {
            return false;
        }
    }


    public static void Vibrate ()
    {
#if UNITY_ANDROID || UNITY_IOS
        
        if ( Application.isMobilePlatform ) {
            Handheld.Vibrate ();
        }

#endif
    }

    public static int AndroidVersion {
        get {
            int iVersionNumber = 0;
            if ( Application.platform == RuntimePlatform.Android ) {
                string androidVersion = SystemInfo.operatingSystem;
                int sdkPos = androidVersion.IndexOf ( "API-" );
                iVersionNumber = int.Parse ( androidVersion.Substring ( sdkPos + 4, 2 ).ToString () );
            }
            return iVersionNumber;
        }
    }
}

public enum ImpactFeedbackStyle
{
    Heavy,
    Medium,
    Light,
    Rigid,
    Soft
}

public enum NotificationFeedbackStyle
{
    Error,
    Success,
    Warning
}
