using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoon : MonoBehaviour
{
    public GameObject popup;
    public GameObject menuButtons;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            popup.SetActive(false);
            menuButtons.SetActive(true);
        }
    }
}
