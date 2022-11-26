using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] string andriodID = "4137731";
    void Start()
    {
        Advertisement.Initialize(andriodID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
