using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> error_images;
    public GameObject game_over_popup;

    int lives = 0;
    int error_number = 0;

    void Start()
    {
        lives = error_images.Count;
        error_number = 0;

    }
    
    private void WrongNumber()
    {
        if(error_number < error_images.Count)
        {
            error_images[error_number].SetActive(true);
            error_number++;
            lives--;
        }

        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if(lives <= 0)
        {
            GameEvents.OnGameOverMethod();
            game_over_popup.SetActive(true);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
    }
}
