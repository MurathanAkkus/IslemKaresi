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
        showBannerAd();
        MobileAds.Initialize(initStatus => { });
    }

    public void showBannerAd()
    {
        requestBannerAd();

        bannerAd.Show();
    }

    public void requestBannerAd()
    {
        bannerAd = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest.Builder().Build();

        // Burada banner reklamýmýzýn AdMobdan yüklüyoruz ve göstermek için hazýr hale getiriyoruz.
        bannerAd.LoadAd(adRequest);
    }
}
