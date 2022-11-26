using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public int value = 0;

    GameObject number_button;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.UpdateSquareNumberMethod(value);
        
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    public void Active(bool set_)
    {
        if (!set_)
            MenuButtons.Instance.DeActivateObject(this);
    }
}
