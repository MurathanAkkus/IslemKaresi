using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using Random = UnityEngine.Random;

public class AdMobAds : MonoBehaviour
{
    string bannerAdId = "ca-app-pub-3940256099942544/6300978111";
    string continueGameAdId = "ca-app-pub-3940256099942544/5354046379";
    string gameOverAdId = "ca-app-pub-3940256099942544/1033173712";
    string giveAHintAdId = "ca-app-pub-3940256099942544/5224354917";

    public BannerView bannerAd;
    public InterstitialAd gameOverAd;
    public RewardedAd giveAHintAd;
    public RewardedInterstitialAd continueGameAd;

    public GameObject failed_load;
    public GameObject failed_show;
    public GameObject not_ready;
    public GameObject gameOverPopup;

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
        requestContinueGameAd();
        requestGameOverAd();
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
        Clock.instance.OnGameOver();
        //int rand = Random.Range(0, 13);
        //if(rand == 1)
        //{
            gameOverAd = new InterstitialAd(gameOverAdId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            gameOverAd.LoadAd(adRequest);
        //}
    }

    public void requestGiveAHintAd()
    {
        giveAHintAd = new RewardedAd(giveAHintAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        giveAHintAd.OnAdLoaded += (sender, args) => { giveAHintAd.Show(); };

        giveAHintAd.OnUserEarnedReward += HandleUserEarnedReward;

        giveAHintAd.OnAdOpening += HandleRewardedAdOpening;

        giveAHintAd.OnAdClosed += HandleRewardedAdClosed;

        giveAHintAd.LoadAd(adRequest);
    }

    public void requestContinueGameAd()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd(continueGameAdId, request, adLoadCallback);
    }

    private void adLoadCallback(RewardedInterstitialAd arg1, AdFailedToLoadEventArgs arg2)
    {
        if (arg2 == null)
            continueGameAd = arg1;
    }

    public void ShowRewardedInterstitialAd()
    {
        if (continueGameAd != null)
            continueGameAd.Show(userEarnedRewardCallback);
        
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        Debug.Log("Continue With Ad");

        gameOverPopup.SetActive(false);
        GameSettings.Instance.SetContinuePreviousGame(true);
        Lives.instance.ResetLives();
        Clock.instance.StartClock();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        Clock.instance.StartClock();
        CreateAndLoadRewardedAd();
        ad_start = false;
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

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("HandleRewardedAdFailedToLoad");

        failed_load.SetActive(true);
    }

    public void HandleOnAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.LogError("HandleRewardedAdFailedToShow");

        failed_show.SetActive(true);
    }

    public void CreateAndLoadRewardedAd()
    {
        RewardedAd giveAHintAd = new RewardedAd(giveAHintAdId);

        giveAHintAd.OnUserEarnedReward += HandleUserEarnedReward;
        giveAHintAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        giveAHintAd.LoadAd(request);
    }
}
