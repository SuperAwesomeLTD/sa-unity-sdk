using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private IAdController adController;
    public MonoBehaviour mainController;

    // Use this for initialization
    void Start()
    {
        adController = (IAdController)mainController;

        Debug.Log("Controller name: " + adController.Name());
    }

    public void OnBannerClick()
    {
        adController.OnBannerClick();
    }

    public void OnInterstitialClick()
    {
        adController.OnInterstitialClick();
    }

    public void OnVideoClick()
    {
        adController.OnVideoClick();
    }
}
