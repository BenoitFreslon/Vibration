////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationExample : MonoBehaviour
{
    public Text inputTime;
    public Text inputPattern;
    public Text inputRepeat;
    public Text txtAndroidVersion;

    // Use this for initialization
    void Start ()
    {
        Vibration.Init ();
        Debug.Log ( "Application.isMobilePlatform: " + Application.isMobilePlatform );
        txtAndroidVersion.text = "Android Version: " + Vibration.AndroidVersion.ToString ();
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void TapVibrate ()
    {
        Vibration.Vibrate ();
    }

    public void TapVibrateCustom ()
    {
#if UNITY_ANDROID
        Vibration.VibrateAndroid ( int.Parse ( inputTime.text ) );
#endif
    }

    public void TapVibratePattern ()
    {
        string[] patterns = inputPattern.text.Replace ( " ", "" ).Split ( ',' );
        long[] longs = Array.ConvertAll<string, long> ( patterns, long.Parse );

        Debug.Log ( longs.Length );
        //Vibration.Vibrate ( longs, int.Parse ( inputRepeat.text ) );
#if UNITY_ANDROID
        Vibration.VibrateAndroid ( longs, int.Parse ( inputRepeat.text ) );
#endif
    }

    public void TapCancelVibrate ()
    {
#if UNITY_ANDROID
        Vibration.CancelAndroid();
#endif
    }

    public void TapPopVibrate ()
    {
        Vibration.VibratePop ();
    }

    public void TapPeekVibrate ()
    {
        Vibration.VibratePeek ();
    }

    public void TapNopeVibrate ()
    {
        Vibration.VibrateNope ();
    }
}
