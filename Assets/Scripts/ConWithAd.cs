using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class ConWithAd : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        button.interactable = true;
    }

    private void OnButtonClicked()
    {
        AdMobAds.instance.requestContinueGameAd();
    }

    private void OnEnable()
    {
        GameEvents.OnGiveAHintAdOpenning += OnConWithAdOpenning;
    }

    private void OnDisable()
    {
        GameEvents.OnGiveAHintAdOpenning -= OnConWithAdOpenning;
    }

    private void OnConWithAdOpenning()
    {
        button.interactable = false;
    }
}

