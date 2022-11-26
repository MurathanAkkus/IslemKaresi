using System.Collections.Generic;
using UnityEngine;

public class NumberButtons : MonoBehaviour
{
    public List<GameObject> numberButtons = new List<GameObject>();
    
    public static NumberButtons instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
