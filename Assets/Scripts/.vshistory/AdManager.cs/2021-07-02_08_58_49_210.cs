using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public string AppID;
    public string BannerAdId;
    public string InterstitialAdId;

    public AdPosition BannerPosition;
    public bool TestDevice = false;

    public static AdManager Instance;

    private BannerView _bannerView;
    private InterstitialAd _interstitial;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    void Start()
    {
        MobileAds.Initialize(AppID);

        this.CreateInterstitialAd(CreateRequest());
    }

    private AdRequest CreateRequest()
    {
        AdRequest request;

        if (TestDevice)
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        else
            request = new AdRequest.Builder().Build();

        return request;
    }

    #region InterstitialAd

    public void CreateInterstitialAd(AdRequest request)
    {
        this._interstitial = new InterstitialAd(InterstitialAdId);
        this._interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this._interstitial.IsLoaded())
            this._interstitial.Show();

        this._interstitial.LoadAd(CreateRequest());
    }

    #endregion

    #region BannerAd

    public void CreateBanner

    #endregion
}
