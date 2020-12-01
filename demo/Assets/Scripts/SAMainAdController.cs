using System;
using System.Collections;
using System.Collections.Generic;
using tv.superawesome.sdk.publisher;
using UnityEngine;
using UnityEngine.UI;

public class SAMainAdController : MonoBehaviour, IAdController
{
    public Text statusText;

    private int bannerPlacementId = 44258;
    private int interstitialPlacementId = 44259;
    private int videoPlacementId = 44262;

    private SABannerAd banner = null;
    private bool enableTestMode = false;
    private Action<int, SAEvent> bannerCallback, interstitialCallback, videoCallback;

    // Start is called before the first frame update
    void Start()
    {
        AwesomeAds.init(true);
        statusText.text = "Initialising";
        prepareCallback();

        ConfigureBannerAd();
        ConfigureInterstitialAd();
        ConfigureVideoAd();
    }

    void prepareCallback()
    {
        bannerCallback = (placementId, evt) =>
        {
            if (evt == SAEvent.adLoaded)
            {
                // set a size template
                banner.setSize_320_50();

                // set a background color
                banner.setColorGray();

                // choose between top or bottom
                banner.setPositionTop();

                // display the ad
                banner.play();
            }

            statusText.text = String.Format("Banner event: {0}", evt);
        };

        interstitialCallback = (placementId, evt) =>
        {
            if (evt == SAEvent.adLoaded)
            {
                // display the ad
                SAInterstitialAd.play(interstitialPlacementId);
            }

            statusText.text = String.Format("Interstitial event: {0}", evt);
        };

        videoCallback = (placementId, evt) =>
        {
            if (evt == SAEvent.adLoaded)
            {
                SAVideoAd.play(videoPlacementId);
            }

            statusText.text = String.Format("Video event: {0}", evt);
        };
    }

    void ConfigureBannerAd()
    {
        // create a new banner
        banner = SABannerAd.createInstance();

        // to display test ads
        if(enableTestMode)
        {
            banner.enableTestMode();
        }

        // ask users to add two numbers when clicking on an ad
        banner.enableParentalGate();

        banner.setCallback(bannerCallback);
    }

    void ConfigureInterstitialAd()
    {
        // set configuration production
        SAInterstitialAd.setConfigurationProduction();
        SAInterstitialAd.enableParentalGate();
        
        // to display test ads
        if (enableTestMode)
        {
            SAInterstitialAd.enableTestMode();
        }

        // lock orientation to portrait or landscape
        SAInterstitialAd.setOrientationPortrait();

        // enable or disable the android back button
        SAInterstitialAd.enableBackButton();

        SAInterstitialAd.setCallback(interstitialCallback);
    }

    void ConfigureVideoAd()
    {
        // make whole video surface clickable
        SAVideoAd.disableSmallClickButton();
        SAVideoAd.enableParentalGate();
        //SAVideoAd.enableCloseButtonWithWarning();

        // set config production
        SAVideoAd.setConfigurationProduction();

        // to display test ads
        if (enableTestMode)
        {
            SAVideoAd.enableTestMode();
        }

        // lock orientation to portrait or landscape
        SAVideoAd.setOrientationPortrait();

        // enable or disable the android back button
        SAVideoAd.enableBackButton();

        // enable or disable a close button
        SAVideoAd.enableCloseButton();

        // enable or disable auto-closing at the end
        SAVideoAd.disableCloseAtEnd();

        SAVideoAd.setCallback(videoCallback);
    }

    public void OnBannerClick()
    {
        Debug.Log("OnBannerClick");
        // start loading ad data for a placement
        banner.load(bannerPlacementId);
    }

    public void OnInterstitialClick()
    {
        // start loading ad data for a placement
        SAInterstitialAd.load(interstitialPlacementId);
    }

    public void OnVideoClick()
    {
        // start loading ad data for a placement
        SAVideoAd.load(videoPlacementId);
    }

    public string Name()
    {
        return "AwesomeAds Controller";
    }
}
