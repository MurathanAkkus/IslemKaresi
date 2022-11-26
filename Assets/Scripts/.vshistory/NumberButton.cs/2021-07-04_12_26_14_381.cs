using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public int value = 0;
    public GameObject number_button;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.UpdateSquareNumberMethod(value);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        //DeActiveNumberButton();
    }

    private void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    public void DeActiveNumberButton()
    {
        number_button.SetActive(false);
    }

    public void NumberButtonControl()
    {
        DeActiveNumberButton();
    }
}
