using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
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

    public GameObject failed_load;
    public GameObject failed_show;

    public static AdMobAds instance;

    public bool ad_start = false;

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

    public void requestGameOverAd()
    {
        gameOverAd = new InterstitialAd(gameOverAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        gameOverAd.LoadAd(adRequest);

        // Reklam yüklenmesini bekler ondan sonra reklamý gösterir.
        gameOverAd.OnAdLoaded += (sender, args) => { gameOverAd.Show(); };
    }

    public void requestGiveAHintAd()
    {
        giveAHintAd = new RewardedAd(giveAHintAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        giveAHintAd.LoadAd(adRequest);

        giveAHintAd.OnAdLoaded += (sender, args) => { giveAHintAd.Show(); };

        giveAHintAd.OnUserEarnedReward += HandleUserEarnedReward;

        giveAHintAd.OnAdOpening += HandleRewardedAdOpening;

        giveAHintAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        Clock.instance.StartClock();
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
        Clock.instance.stop_clock = true;
        ad_start = true;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Give A Hint");

        GameEvents.OnGiveAHintMethod();
        Clock.instance.StartClock();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("HandleRewardedAdFailedToLoad");

        failed_load.SetActive(true);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.LogError("HandleRewardedAdFailedToShow");

        failed_show.SetActive(true);
    }
}
