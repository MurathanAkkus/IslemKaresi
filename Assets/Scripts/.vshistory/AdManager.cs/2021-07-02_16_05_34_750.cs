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
    public string RewardedAdsId;
    public AdPosition BannerPosition;
    public bool TestDevice = false;

    public static AdManager Instance;

    private BannerView _bannerView;
    private InterstitialAd _interstitial;
    private RewardedAd hintRewardedAd;

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
        this.CreateBanner(CreateRequest());
        this.CreateInterstitialAd(CreateRequest());
        this.CreateRewardedAd(CreateRequest());

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        hintRewardedAd.OnUserEarnedReward += OnUserEarnedHintReward;
        hintRewardedAd.OnAdOpening += OnUserEarnedHintAdOpenning;
    }

    private void OnDestroy()
    {
        hintRewardedAd.OnUserEarnedReward -= OnUserEarnedHintReward;
        hintRewardedAd.OnAdOpening -= OnUserEarnedHintAdOpenning;
    }

    private void OnUserEarnedHintAdOpenning(object sender, EventArgs e)
    {
        GameEvents.OnGiveAHintAdOpenningMethod();
    }

    private void OnUserEarnedHintReward(object sender, Reward e)
    {
        GameEvents.OnGiveAHintMethod();
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

    #region RewardedAd
    public void CreateRewardedAd(AdRequest request)
    {
        this.hintRewardedAd = new RewardedAd(RewardedAdsId);
        this.hintRewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        if (this.hintRewardedAd.IsLoaded())
            this.hintRewardedAd.Show();
    }

    public bool IsRewardedAdLoaded()
    {
        return this.hintRewardedAd.IsLoaded();
    }

    #endregion

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

    public void CreateBanner(AdRequest request)
    {
        this._bannerView = new BannerView(BannerAdId, AdSize.SmartBanner, BannerPosition);
        this._bannerView.LoadAd(request);
        HideBanner();
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }

    #endregion
}
