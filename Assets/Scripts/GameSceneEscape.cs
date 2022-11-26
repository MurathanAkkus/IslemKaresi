using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneEscape : MonoBehaviour
{
    public GameObject pause_menu;
    public GameObject win_menu;
    public GameObject gameOver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            || (Clock.instance.stop_clock && !win_menu.active
            && !gameOver.active && !AdMobAds.instance.ad_start))
        {
            pause_menu.SetActive(!pause_menu.active);
            if (pause_menu.active == true)
            {
                PauseMenu.Instance.DisplayTime();
                GameSettings.Instance.SetPaused(pause_menu.active);
            }
        }
    }
}