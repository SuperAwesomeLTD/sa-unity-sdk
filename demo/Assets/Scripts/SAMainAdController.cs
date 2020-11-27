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
    private int videoPlacementId = 39318;

    private SABannerAd banner = null;
    private bool enableTestMode = true;
    private Action<int, SAEvent> callback;

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
        callback = (placementId, evt) =>
        {
            switch (evt)
            {

                case SAEvent.adLoaded:
                    statusText.text = String.Format("adLoaded: {0}", placementId);
                    break;
                case SAEvent.adEmpty:
                    statusText.text = String.Format("adEmpty: {0}", placementId);
                    break;
                case SAEvent.adFailedToLoad:
                    statusText.text = String.Format("adFailedToLoad: {0}", placementId);
                    break;
                case SAEvent.adShown:
                    statusText.text = String.Format("adShown: {0}", placementId);
                    break;
                case SAEvent.adFailedToShow:
                    statusText.text = String.Format("adFailedToShow: {0}", placementId);
                    break;
                case SAEvent.adClicked:
                    statusText.text = String.Format("adClicked: {0}", placementId);
                    break;
                case SAEvent.adEnded:
                    statusText.text = String.Format("adEnded: {0}", placementId);
                    break;
                case SAEvent.adClosed:
                    statusText.text = String.Format("adClosed: {0}", placementId);
                    break;
            }
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

        // start loading ad data for a placement
        banner.load(bannerPlacementId);

        banner.setCallback(callback);
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

        // start loading ad data for a placement
        SAInterstitialAd.load(interstitialPlacementId);

        SAInterstitialAd.setCallback(callback);
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

        // start loading ad data for a placement
        SAVideoAd.load(videoPlacementId);

        SAVideoAd.setCallback(callback);
    }

    public void OnBannerClick()
    {
        Debug.Log("OnBannerClick");
        // check if ad is loaded
        if (banner.hasAdAvailable())
        {

            // set a size template
            banner.setSize_320_50();

            // set a background color
            banner.setColorGray();

            // choose between top or bottom
            banner.setPositionTop();

            // display the ad
            banner.play();
        } else
        {
            statusText.text = String.Format("OnBannerClick ad not available");
        }
    }

    public void OnInterstitialClick()
    {

        // check if ad is loaded
        if (SAInterstitialAd.hasAdAvailable(interstitialPlacementId))
        {

            // display the ad
            SAInterstitialAd.play(interstitialPlacementId);
        }else
        {
            statusText.text = String.Format("OnInterstitialClick ad not available");
        }
    }

    public void OnVideoClick()
    {
        // check if ad is loaded
        if (SAVideoAd.hasAdAvailable(videoPlacementId))
        {

            // display the ad
            SAVideoAd.play(videoPlacementId);
        }else
        {
            statusText.text = String.Format("OnVideoClick ad not available");
        }
    }

    public string Name()
    {
        return "AwesomeAds Controller";
    }
}
