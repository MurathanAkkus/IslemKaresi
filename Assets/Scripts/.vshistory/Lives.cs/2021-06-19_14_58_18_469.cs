using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> error_images;

    int lives = 0;
    int error_number = 0;

    void Start()
    {
        lives = error_images.Count;
        error_number = 0;

    }
    
    void Update()
    {
        
    }
}
