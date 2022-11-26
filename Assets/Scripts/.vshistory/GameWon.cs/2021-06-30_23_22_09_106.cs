using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameWon : MonoBehaviour
{
    public GameObject WinPopup;
    public Text ClockText;

    void Start()
    {
        WinPopup.SetActive(false);
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnBoardCompleted()
    {
        WinPopup.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += OnBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
    }
}
