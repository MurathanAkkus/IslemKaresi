using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class GiveAHintButton : MonoBehaviour
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
        AdMobAds.instance.requestGiveAHintAd();
    }

    private void OnEnable()
    {
        GameEvents.OnGiveAHintAdOpenning += OnGiveAHintAdOpenning;
    }

    private void OnDisable()
    {
        GameEvents.OnGiveAHintAdOpenning -= OnGiveAHintAdOpenning;
    }

    private void OnGiveAHintAdOpenning()
    {
        button.interactable = false;
    }
}
