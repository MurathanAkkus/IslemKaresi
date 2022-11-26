using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RubberButton : Selectable, IPointerClickHandler
{   // Silgi
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.OnClearNumberMethod();
    }
}
