using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string andriodID = "4202317";

    //placement Ids
    [SerializeField] string giveAHintAdsPlacementId = "GiveAHintAds";
    [SerializeField] string skippableVideoPlacementId = "SkippableVideo";


    void Start()
    {
        Advertisement.AddListener(this);

        //initalize the ads using the android id
        Advertisement.Initialize(andriodID);
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
