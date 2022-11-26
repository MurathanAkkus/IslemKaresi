using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Grid'in icindeki bir karenin class'i
public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler // UnityEngine.UI class'indan Selectable API'sini cagirarak,
{                                    // basit secilebilir nesne ve kontroller kullanilabilinir.
    public GameObject number_text;
    private string number_ = "0";
    private int correct_number = 0;

    private bool selected_ = false;
    private int square_index_ = -1;

    public bool IsSelected() { return selected_; }
    public void SetSquareIndex(int index)
    {
        square_index_ = index;
    }

    public void SetCorrectNumber(int number)
    {
        correct_number = number;
    }

    void Start()
    {
        selected_ = false;
    }

    private void DisplayText(GameObject square)
    {
        if (number_ == "0")
            number_text.GetComponent<Text>().text = null;
        else if (number_ == "-1")
        {
            number_text.GetComponent<Text>().text = null;
            square.SetActive(false);
        }
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

    public string SetOpt()
    {
        string[] opt = { "+", "-", "*", "/" };
        int number = Random.Range(0, 4);
        return opt[number];
    }

    public void SetOpt_withoutDiv()
    {
        string[] opt = { "+", "-", "*" };
        int number = Random.Range(0, 3);
        number_text.GetComponent<Text>().text = opt[number];
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
        }
    }

    public void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
            selected_ = false;
    }

    internal void SetCorrectNumber(string v)
    {
        throw new System.NotImplementedException();
    }
}

