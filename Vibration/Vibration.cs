////////////////////////////////////////////////////////////////////////////////
//
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using System.Threading.Tasks;

#if UNITY_IOS
using System.Collections;
using System.Runtime.InteropServices;
#endif

#if UNITY_WEBGL
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

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void VibrateWebgl(int ms);

    [DllImport("__Internal")]
    private static extern int HasVibratorWebgl();
#endif

    private static bool initialized = false;
    private static bool isPaused = false;

    public static void Init()
    {
        if (initialized) return;

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

    #region IOS specific methods
    public static void VibrateIOS(ImpactFeedbackStyle style)
    {
        if (!IsAvailable()) return;
#if UNITY_IOS
        _impactOccurred(style.ToString());
#endif
    }

    public static void VibrateIOS(NotificationFeedbackStyle style)
    {
        if (!IsAvailable()) return;
#if UNITY_IOS
        _notificationOccurred(style.ToString());
#endif
    }

    public static void VibrateIOS_SelectionChanged()
    {
        if (!IsAvailable()) return;
#if UNITY_IOS
        _selectionChanged();
#endif
    }
    #endregion

    /// <summary>
    /// Light impact feedback for small interactions.
    /// </summary>
    public static void VibrateLight()
    {
        if (!IsAvailable()) return;
#if UNITY_WEBGL
        VibrateWebgl(25);
#elif UNITY_IOS || UNITY_ANDROID
        if (!Application.isMobilePlatform) return;
#if UNITY_IOS
        _impactOccurred(ImpactFeedbackStyle.Light.ToString());
#elif UNITY_ANDROID
        VibrateAndroid(25);
#endif
#endif
    }

    ///<summary>
    /// Tiny pop vibration
    ///</summary>
    public static void VibratePop()
    {
        if (!IsAvailable()) return;
#if UNITY_WEBGL
        VibrateWebgl(50);
#elif UNITY_IOS || UNITY_ANDROID
        if (!Application.isMobilePlatform) return;
#if UNITY_IOS
        _VibratePop ();
#elif UNITY_ANDROID
        VibrateAndroid ( 50 );
#endif
#endif
    }

    ///<summary>
    /// Small peek vibration
    ///</summary>
    public static void VibratePeek()
    {
        if (!IsAvailable()) return;
#if UNITY_WEBGL
        VibrateWebgl(100);
#elif UNITY_IOS || UNITY_ANDROID
        if (!Application.isMobilePlatform) return;
#if UNITY_IOS
        _VibratePeek ();
#elif UNITY_ANDROID
        VibrateAndroid ( 100 );
#endif
#endif
    }

    ///<summary>
    /// 3 small vibrations
    ///</summary>
    public static Task VibrateNope()
    {
        if (!IsAvailable()) return Task.CompletedTask;
#if UNITY_WEBGL
        return VibrateNopeWebgl();
#elif UNITY_IOS || UNITY_ANDROID
        if (Application.isMobilePlatform)
        {
#if UNITY_IOS
            _VibrateNope ();
#elif UNITY_ANDROID
            long[] pattern = { 0, 50, 50, 50 };
            VibrateAndroid ( pattern, -1 );
#endif
        }
#endif
        return Task.CompletedTask;
    }

#if UNITY_WEBGL
    private static async Task VibrateNopeWebgl()
    {
        VibrateWebgl(50);
        await WaitForWebglVibrationInterval();
        VibrateWebgl(50);
        await WaitForWebglVibrationInterval();
        VibrateWebgl(50);
    }

    private static async Task WaitForWebglVibrationInterval()
    {
        float endTime = Time.realtimeSinceStartup + 0.05f;
        while (Time.realtimeSinceStartup < endTime)
            await Task.Yield();
    }
#endif

    #region Android specific methods
#if UNITY_ANDROID
    ///<summary>
    /// Only on Android
    /// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
    ///</summary>
    public static void VibrateAndroid ( long milliseconds )
    {
        if (!IsAvailable()) return;
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
        if (!IsAvailable()) return;
        if ( Application.isMobilePlatform ) {
            if ( AndroidVersion >= 26 ) {
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
    public static void CancelAndroid()
    {
        if (Application.isMobilePlatform)
        {
#if UNITY_ANDROID
            vibrator.Call ( "cancel" );
#endif
        }
    }
    #endregion

    public static bool HasVibrator()
    {
#if UNITY_WEBGL
        return HasVibratorWebgl() != 0;
#elif UNITY_ANDROID
        if (!Application.isMobilePlatform) return false;

        AndroidJavaClass contextClass = new AndroidJavaClass ( "android.content.Context" );
        string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string> ( "VIBRATOR_SERVICE" );
        AndroidJavaObject systemService = context.Call<AndroidJavaObject> ( "getSystemService", Context_VIBRATOR_SERVICE );
        return systemService.Call<bool> ( "hasVibrator" );
#elif UNITY_IOS
        return Application.isMobilePlatform && _HasVibrator ();
#else
        return false;
#endif
    }

    public static void Vibrate()
    {
        if (!IsAvailable()) return;
#if UNITY_ANDROID || UNITY_IOS
        if ( Application.isMobilePlatform ) {
            Handheld.Vibrate ();
        }
#elif UNITY_WEBGL
        VibrateWebgl(400);
#endif
    }

    public static int AndroidVersion
    {
        get
        {
            if (Application.platform != RuntimePlatform.Android)
                return 0;

            string androidVersion = SystemInfo.operatingSystem;
            const string apiMarker = "API-";
            int apiMarkerIndex = androidVersion.IndexOf(apiMarker, System.StringComparison.Ordinal);
            if (apiMarkerIndex < 0)
                return 0;

            int versionStart = apiMarkerIndex + apiMarker.Length;
            int versionEnd = versionStart;
            while (versionEnd < androidVersion.Length && androidVersion[versionEnd] >= '0' && androidVersion[versionEnd] <= '9')
                versionEnd++;

            if (versionEnd == versionStart)
                return 0;

            int version;
            return int.TryParse(androidVersion.Substring(versionStart, versionEnd - versionStart), out version) ? version : 0;
        }
    }

    public static void SetActive(bool active)
    {
        isPaused = !active;
        if (isPaused)
        {
            CancelAndroid();
        }
    }

    public static bool IsAvailable()
    {
        if (!HasVibrator()) return false;
        if (isPaused) return false;
        return true;
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
