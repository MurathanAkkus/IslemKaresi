using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadPlayAgain()
    {
        string level = GameSettings.Instance.GetGameMode();
        string name = "GameScene";

        if (level.Equals("Easy"))
            LoadEasyGame(name);
        else if (level.Equals("Medium"))
            LoadMediumGame(name);
        else if (level.Equals("Hard"))
            LoadHardGame(name);
        else if (level.Equals("VeryHard"))
            LoadVeryHardGame(name);
        else
            LoadScene("MainMenu");
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadEasyGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.EASY);
        SceneManager.LoadScene(name);
    }

    public void LoadMediumGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.MEDIUM);
        SceneManager.LoadScene(name);
    }

    public void LoadHardGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.HARD);
        SceneManager.LoadScene(name);
    }

    public void LoadVeryHardGame(string name)
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.VERY_HARD);
        SceneManager.LoadScene(name);
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetPause(bool paused)
    {
        GameSettings.Instance.SetPaused(paused);
    }

    public void ContinuePreviousGame(bool continue_game)
    {
        GameSettings.Instance.SetContinuePreviousGame(continue_game);
    }

    public void ExitAfterWon(bool tick)
    {
        GameSettings.Instance.SetExitAfterWon(tick);
    }

    public void ContinueAfterGameOver()
    {
        AdManager.Instance.ShowInterstitialAd();
        Lives.instance.ResetLives();
    }
}