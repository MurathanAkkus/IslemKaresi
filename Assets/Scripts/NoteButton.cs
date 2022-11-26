using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : Selectable, IPointerClickHandler
{
    public Sprite on_image;    
    public Sprite off_image;
    private bool active;

    void Start()
    {
        active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;

        if (active)
            GetComponent<Image>().sprite = on_image;
        else
            GetComponent<Image>().sprite = off_image;

        GameEvents.OnNotesActiveMethod(active);
    }
}
