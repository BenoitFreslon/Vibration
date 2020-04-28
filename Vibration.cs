////////////////////////////////////////////////////////////////////////////////
//
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
#if UNITY_IOS && !UNITY_EDITOR
using System.Collections;
using System.Runtime.InteropServices;
#endif

public static class Vibration
{
#if UNITY_IOS && !UNITY_EDITOR
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

	///<summary>
	/// Tiny pop vibration
	///</summary>
    public static void VibratePop ()
    {
		#if UNITY_IOS && !UNITY_EDITOR
        _VibratePop ();
		#elif UNITY_ANDROID  && !UNITY_EDITOR
		Vibrate(15);
		#endif
    }
	///<summary>
	/// Small peek vibration
	///</summary>
    public static void VibratePeek ()
    {
		#if UNITY_IOS && !UNITY_EDITOR
        _VibratePeek ();
		#elif UNITY_ANDROID  && !UNITY_EDITOR
		Vibrate ( 25 );
		#endif
    }
	///<summary>
	/// 3 small vibrations
	///</summary>
    public static void VibrateNope ()
    {
		#if UNITY_IOS && !UNITY_EDITOR
        _VibrateNope ();
		#elif UNITY_ANDROID  && !UNITY_EDITOR
		long [] pattern = { 0, 5, 5, 5 };
		Vibrate( pattern, -1 );
		#endif
    }

#if UNITY_ANDROID && !UNITY_EDITOR
	public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	public static AndroidJavaObject vibrator =currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
	public static AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
#endif
	///<summary>
	/// Only on Android
	/// https://developer.android.com/reference/android/os/Vibrator.html#vibrate(long)
	///</summary>
	public static void Vibrate(long milliseconds)
	{
#if !UNITY_WEBGL
		#if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("vibrate", milliseconds);
		#elif UNITY_IOS && !UNITY_EDITOR
        Handheld.Vibrate();
		#else
		Handheld.Vibrate();
		#endif
#endif
	}

	///<summary>
	/// Only on Android
	/// https://proandroiddev.com/using-vibrate-in-android-b0e3ef5d5e07
	///</summary>
	public static void Vibrate(long[] pattern, int repeat)
	{
#if !UNITY_WEBGL
		#if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("vibrate", pattern, repeat);
		#elif UNITY_IOS && !UNITY_EDITOR
        Handheld.Vibrate();
		#else
		Handheld.Vibrate();
		#endif
#endif
	}

	///<summary>
	///Only on Android
	///</summary>
	public static void Cancel()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		vibrator.Call("cancel");
		#endif
	}

	public static bool HasVibrator()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
		string Context_VIBRATOR_SERVICE = contextClass.GetStatic<string>("VIBRATOR_SERVICE");
		AndroidJavaObject systemService = context.Call<AndroidJavaObject>("getSystemService", Context_VIBRATOR_SERVICE);
		if (systemService.Call<bool>("hasVibrator"))
		{
			return true;
		}
		else
		{
			return false;
		}
#elif UNITY_IOS && !UNITY_EDITOR
        return _HasVibrator ();
#else
		return false;
#endif
	}

	public static void Vibrate()
	{
#if !UNITY_WEBGL
		Handheld.Vibrate();
#endif
	}
}
