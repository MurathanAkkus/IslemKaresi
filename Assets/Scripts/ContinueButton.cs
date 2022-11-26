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
            timeText.text = " ";
            levelText.text = " ";
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

            string level = Config.ReadBoardLevel();
            if (level.Equals("Easy"))
                level = "Kolay";
            else if (level.Equals("Medium"))
                level = "Orta";
            else if (level.Equals("Hard"))
                level = "Zor";
            else
                level = "Uzman";

            levelText.text = level;
        }
    }

    public void SetGameData() 
    {
        GameSettings.Instance.SetGameMode(Config.ReadBoardLevel());
    }
}
