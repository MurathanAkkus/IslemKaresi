using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using Random = UnityEngine.Random;

public class AdMobAds : MonoBehaviour
{
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
        int rand = Random.Range(0, 13);
        if(rand == 1)
        {
            ad_start = true;
            gameOverAd = new InterstitialAd(gameOverAdId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            gameOverAd.LoadAd(adRequest);

            // Reklam yüklenmesini bekler ondan sonra reklamý gösterir.
            gameOverAd.OnAdLoaded += (sender, args) => { gameOverAd.Show(); };

            // Called when an ad request failed to load.
            gameOverAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            ad_start = false;
        }
    }

    public void requestGiveAHintAd()
    {
        ad_start = true;
        giveAHintAd = new RewardedAd(giveAHintAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        giveAHintAd.LoadAd(adRequest);

        giveAHintAd.OnAdLoaded += (sender, args) => { giveAHintAd.Show(); };

        giveAHintAd.OnUserEarnedReward += HandleUserEarnedReward;

        giveAHintAd.OnAdOpening += HandleRewardedAdOpening;

        giveAHintAd.OnAdClosed += HandleRewardedAdClosed;
        ad_start = false;
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
        {
            continueGameAd.Show(userEarnedRewardCallback);
        }
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
        giveAHintAd = new RewardedAd(CreateAndLoadRewardedAd());
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

    public RewardedAd CreateAndLoadRewardedAd()
    {
        RewardedAd rewardedAd = new RewardedAd(giveAHintAdId);

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }
}
