using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ContinueButton : MonoBehaviour
{
    public Text timeText;
    public Text levelText;
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
        }
    }

    
    void Update()
    {
        
    }
}
