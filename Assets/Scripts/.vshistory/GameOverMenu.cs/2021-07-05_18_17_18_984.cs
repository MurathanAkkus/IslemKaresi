using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;
    public GameObject this_;

    public static GameOverMenu Instance;

    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
