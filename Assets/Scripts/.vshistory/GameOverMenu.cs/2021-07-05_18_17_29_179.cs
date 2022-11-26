using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;
    public GameObject this_;

    public static GameOverMenu Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
