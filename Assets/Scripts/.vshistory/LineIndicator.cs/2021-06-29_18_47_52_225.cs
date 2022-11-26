using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    public static LineIndicator instance;

    private int[,]

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
