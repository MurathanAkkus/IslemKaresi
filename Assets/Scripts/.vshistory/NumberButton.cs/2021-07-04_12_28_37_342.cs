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

    void Start()
    {
        number_button.SetActive(true);
    }

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
        GameEvents.OnNumberButtonControl += OnNumberButtonControl;
    }

    private void OnDisable()
    {
        GameEvents.OnNumberButtonControl -= OnNumberButtonControl;
    }

    public void DeActiveNumberButton()
    {
        number_button.SetActive(false);
    }

    public void OnNumberButtonControl()
    {
        DeActiveNumberButton();
    }
}
