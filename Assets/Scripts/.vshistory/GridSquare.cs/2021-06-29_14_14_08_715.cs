using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Grid'in icindeki bir karenin class'i
public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler // UnityEngine.UI class'indan Selectable API'sini cagirarak,
{                                    // basit secilebilir nesne ve kontroller kullanilabilinir.
    public GameObject number_text;
    private string number_;
    private string correct_number;

    private bool selected_ = false;
    private int square_index_ = -1;
    private bool has_default_value = false;
    private bool has_wrong_value = false;

    public bool HasWrongValue() { return has_wrong_value; }
    public void SetHasDefaultValue(bool has_default) { has_default_value = has_default; }
    public bool GetHasDefaultValue() { return has_default_value; }

    public bool IsSelected() { return selected_; }
    public void SetSquareIndex(int index)
    {
        square_index_ = index;
    }

    public void SetCorrectNumber(string number)
    {
        correct_number = number;
        has_wrong_value = false;
    }

    void Start()
    {
        selected_ = false;
    }

    private void DisplayText(GameObject square)
    {
        if (number_ == "_")
            square.SetActive(false);
        else
            number_text.GetComponent<Text>().text = number_; // Buraki <> icindeki Text, UnityEngine.UI 'den cagrildi
    }


    public void SetNumber(string number, GameObject square)
    {
        number_ = number;
        DisplayText(square);
    }

    public void SetNumber(string number)
    {
        number_text.GetComponent<Text>().text = number; // Buraki <> icindeki Text, UnityEngine.UI 'den cagrildi
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.SquareSelectedMethod(square_index_);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    public void OnSetNumber(int number)
    {
        if(selected_)
        {
            SetNumber(number.ToString());

            if (number_ != correct_number)
            {
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                has_default_value = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
            }
        }
    }

    public void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
            selected_ = false;
    }
}

