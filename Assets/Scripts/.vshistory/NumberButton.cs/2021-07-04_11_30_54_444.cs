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

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void DeActiveNumberButton()
    {
        MenuButtons.Instance.DeActivateObject(number_button);
    }

    public void NumberButtonControl(int square_index)
    {
        List<GameObject> squares = IkGrid.Instance.grid_squares_;
        GridSquare selected_square = squares[square_index].GetComponent<GridSquare>();

        if (!selected_square.GetHasDefaultValue() && !selected_square.IsOpt_Square() && selected_square.IsCorrectNumberSet())
            DeActiveNumberButton();
    }
}
