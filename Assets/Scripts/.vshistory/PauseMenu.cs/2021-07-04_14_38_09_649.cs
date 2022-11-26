using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text time_text;

    public static PauseMenu Instance;

    public void DisplayTime()
    {
        time_text.text = Clock.instance.GetCurrentTimeText().text;
    }
}
