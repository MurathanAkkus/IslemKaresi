using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text time_text;

    public static PauseMenu Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void DisplayTime()
    {
        time_text.text = Clock.instance.GetCurrentTimeText().text;
    }
}
