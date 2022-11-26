using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RubberButton : Selectable, IPointerClickHandler
{   // Silgi
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.OnClearNumberMethod();
    }
}
