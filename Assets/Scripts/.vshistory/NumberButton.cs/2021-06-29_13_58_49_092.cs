using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    private int value = 0;

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

    public interface ISubmitHandler : IEventSystemHandler
    {
        void OnSubmit(BaseEventData eventData);
    }
}
