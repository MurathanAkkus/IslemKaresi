using UnityEngine;

public class Close : MonoBehaviour
{
    public GameObject popup;
    public GameObject menuButtons;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            popup.SetActive(false);
            menuButtons.SetActive(true);
            Clock.instance.StartClock();
        }
    }
}
