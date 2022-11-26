using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string andriodID = "4202317";

    //placement Ids
    [SerializeField] string giveAHintAdsPlacementId = "GiveAHintAds";
    [SerializeField] string gameOverAndConPlacementId = "GameOverAndCon";

    public GameObject gameOverPopup;

    public static AdsManager Instance;

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
        Advertisement.AddListener(this);

        //initalize the ads using the android id
        Advertisement.Initialize(andriodID);
    }

    public void ShowGiveAHintAd()
    {
        Advertisement.Show(giveAHintAdsPlacementId);
    }

    public void GameOverAndConAd()
    {
        Advertisement.Show(gameOverAndConPlacementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {   //check if the player finished watching the ad
        if(showResult == ShowResult.Finished)
        {
            //check if the ad that was watched is the give a hint video ad
            if(placementId == giveAHintAdsPlacementId)
            {
                //in that case
                //debug in the console to check if it's working
                Debug.Log("Give A Hint");
                GameEvents.OnGiveAHintMethod();
            }
            else if(placementId == gameOverAndConPlacementId)
            {
                Debug.Log("Continue With Ad");
                GameSettings.Instance.SetContinuePreviousGame(true);
                Clock.instance.StartClock();
                gameOverPopup.SetActive(false);
            }
        }
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
