using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;

    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
