using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Use AndroidHttpsHelper.AddCertificate with the content of your .cert file to allow access to your HTTPS page
/// using Unity's the WWW class.
/// 
/// 
/// </summary>
public class AndroidHttpsHelper
{
    private static readonly string JAVA_CLASS_NAME = "co.fourscience.androidhttpsplugin.AndroidHttps";

    public static void IgnoreCertificates()
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsJavaSSLHelper = new AndroidJavaClass(JAVA_CLASS_NAME);
            clsJavaSSLHelper.CallStatic("ignoreCertifcates");
        }
#endif
    }

    public static void TrustOnly(String certFileContent)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsJavaSSLHelper = new AndroidJavaClass(JAVA_CLASS_NAME);
            byte[] certBytes = System.Text.Encoding.ASCII.GetBytes(certFileContent);
            clsJavaSSLHelper.CallStatic("trustOnly", certBytes);
        }
#endif
    }

    public static void AddCertificate(String certFileContent)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass clsJavaSSLHelper = new AndroidJavaClass(JAVA_CLASS_NAME);
            byte[] certBytes = System.Text.Encoding.ASCII.GetBytes(certFileContent);
            clsJavaSSLHelper.CallStatic("addCertificate", certBytes);
        }
#endif
    }
}