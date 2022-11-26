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

    public void OnSquareSelected(int square_index)
    {
        List<GameObject> squares = IkGrid.Instance.grid_squares_;
        GridSquare selected_square = squares[square_index].GetComponent<GridSquare>();

        if (!selected_square.GetHasDefaultValue() && !selected_square.IsOpt_Square() && selected_square.IsCorrectNumberSet())
            DeActiveNumberButton();
    }
}
