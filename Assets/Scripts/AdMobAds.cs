using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;

public class AdMobAds : MonoBehaviour
{
    string continueGameAdId = "ca-app-pub-3024251209916393/1540024485";
    string giveAHintAdId = "ca-app-pub-3024251209916393/9842793599";

    public RewardedAd giveAHintAd;
    public RewardedAd continueGameAd;

    public List<GameObject> gameOverPopup = new List<GameObject>();

    public static AdMobAds instance;

    public bool ad_start = false;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
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
        continueGameAd = new RewardedAd(continueGameAdId);

        AdRequest adRequest = new AdRequest.Builder().Build();

        continueGameAd.OnAdLoaded += (sender, args) => { continueGameAd.Show(); };

        continueGameAd.OnUserEarnedReward += HandleUserEarnedConRew;

        continueGameAd.LoadAd(adRequest);
    }


    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        Clock.instance.StartClock();
        ad_start = false;
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
        Clock.instance.stop_clock = true;
        ad_start = true;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameEvents.OnGiveAHintMethod();
        Clock.instance.StartClock();
    }

    public void HandleUserEarnedConRew(object sender, Reward args)
    {
        Debug.Log("Continue With Ad");
        GameOverMenu.instance.SetActiveGamePopup(false);
        Lives.instance.ResetLives();
        GameSettings.Instance.SetContinuePreviousGame(true);
        Clock.instance.StartClock();
        Debug.Log("Geldik");
    }
}