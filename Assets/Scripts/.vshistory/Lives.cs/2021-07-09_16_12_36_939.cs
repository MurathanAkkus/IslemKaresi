using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> error_images;
    public GameObject game_over_popup;

    int lives = 0;
    int error_number = 0;
    public static Lives instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }

    void Start()
    {
        lives = error_images.Count;
        error_number = 0;

        if (GameSettings.Instance.GetContinuePreviousGame())
        {
            error_number = Config.ErrorNumber();
            lives = error_images.Count - error_number;

            for (int error = 0; error < error_number; error++)
                error_images[error].SetActive(true);
        }
    }

    public int GetErrorNumbers() { return error_number; }

    private void WrongNumber()
    {
        if (error_number < error_images.Count)
        {
            error_images[error_number].SetActive(true);
            error_number++;
            lives--;
        }

        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (lives <= 0)
        {
            GameEvents.OnGameOverMethod();
            AdsManager.Instance.ShowSkippableAd();
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

    public void ResetLives()
    {
        error_number--;
        error_images[error_number].SetActive(false);

        lives++;
    }
}
