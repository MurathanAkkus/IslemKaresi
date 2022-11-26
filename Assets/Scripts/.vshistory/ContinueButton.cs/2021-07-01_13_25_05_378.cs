using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ContinueButton : MonoBehaviour
{
    public Text timeText;
    public Text levelText;

    string LeadingZero(int n) { return n.ToString().PadLeft(2, '0'); }

    void Start()
    {
        if(!Config.GameDataFileExist())
        {
            gameObject.GetComponent<Button>().interactable = false;
            timeText.text = null;
            levelText.text = null;
        }
        else
        {
            float delta_time = Config.ReadGameTime();
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            timeText.text = hour + ":" + minute + ":" + seconds;

            levelText.text = Config.ReadBoardLevel();
        }
    }

    public void SetGameData() 
    {
        GameSettings.Instance.SetGameMode(Config.ReadBoardLevel());
    }
}
