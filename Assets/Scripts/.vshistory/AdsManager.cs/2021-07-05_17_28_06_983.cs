using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string andriodID = "4202317";

    //placement Ids
    [SerializeField] string giveAHintAdsPlacementId = "GiveAHintAds";
    [SerializeField] string continueWithAdsPlacementId = "ContinueWithAds";
    [SerializeField] string skippableVideoPlacementId = "SkippableVideo";
    

    void Start()
    {
        Advertisement.AddListener(this);

        //initalize the ads using the android id
        Advertisement.Initialize(andriodID);
    }

    public void ShowGiveAHintAd()
    {
        Advertisement.Show(giveAHintAdsPlacementId);
    }

    public void ShowContinueWithAd()
    {
        Advertisement.Show(continueWithAdsPlacementId);
    }

    public void ShowSkippableAd()
    {
        //show skippable ad
        Advertisement.Show(skippableVideoPlacementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }
}
