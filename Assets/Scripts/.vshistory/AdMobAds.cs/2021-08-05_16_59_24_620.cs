using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;

public class AdMobAds : MonoBehaviour
{
    string GameID = " ";

    string bannerAdId = "ca-app-pub-3024251209916393/3400743982";
    string continueGameAdId = "ca-app-pub-3024251209916393/4089286923";
    string gameOverAdId = "ca-app-pub-3024251209916393/3468956932";
    string giveAHintAdId = "ca-app-pub-3024251209916393/9842793599";

    public BannerView bannerAd;
    public InterstitialAd gameOverAd;
    public RewardedAd giveAHintAd;
    public RewardedInterstitialAd continueGameAd;

    public static AdMobAds instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        MobileAds.Initialize(GameID);
    }

    public void reqBannerAd()
    {
        this.bannerAd = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);

        this.bannerAd.OnAdLoaded += this.HandleOnAdLoaded;
    }
}
