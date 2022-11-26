using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;
    public GameObject this_;

    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
