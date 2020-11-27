using UnityEngine;
using System.Collections;

interface IAdController
{
    string Name();

    void OnBannerClick();

    void OnInterstitialClick();

    void OnVideoClick();
}