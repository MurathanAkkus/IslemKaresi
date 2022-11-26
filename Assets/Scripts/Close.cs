using UnityEngine;

public class Close : MonoBehaviour
{
    public GameObject popup;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            popup.SetActive(false);
            Clock.instance.StartClock();
        }
    }
}
