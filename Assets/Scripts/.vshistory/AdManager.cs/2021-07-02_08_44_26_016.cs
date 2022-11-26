using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public string AppID;
    public string BanerAdId;
    public string InterstitialAdId;

    public AdPosition BanePosition;
    public bool TestDevice = false;

    public static AdManager Instance;

    private BannerView _baneView;
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
    }

    private AdRequest CreateRequest()
    {
        AdRequest request;

        if (TestDevice)
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        else
            request = new AdRequest.Builder().Build();
    }
}
