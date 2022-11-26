using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoon : MonoBehaviour
{
    public GameObject popup;
    private int i = 0; // touchCount

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            popup.SetActive(false);
        }
    }
}
