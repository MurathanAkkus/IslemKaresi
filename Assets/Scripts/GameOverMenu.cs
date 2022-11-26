using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;

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
    public Text textClock;
    public GameObject GameOverPopup;
    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void Update()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }

    public void SetActiveGamePopup(bool active)
    {
        if (!active)
            GameOverPopup.SetActive(false);
        else
            GameOverPopup.SetActive(true);
    }
}
