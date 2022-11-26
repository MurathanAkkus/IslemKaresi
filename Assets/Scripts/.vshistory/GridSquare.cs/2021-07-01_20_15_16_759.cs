using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

//Grid'in icindeki bir karenin class'i
public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler // UnityEngine.UI class'indan Selectable API'sini cagirarak,
{                                    // basit secilebilir nesne ve kontroller kullanilabilinir.
    public GameObject number_text;
    public List<GameObject> number_notes;
    private bool note_active;

    private string number_;
    private string correct_number;

    private bool selected_ = false;
    private int square_index_ = -1;
    private bool has_default_value = false;
    private bool has_wrong_value = false;

    public string GetSquareNumber() { return number_text.ToString(); }

    public bool IsCorrectNumberSet() { return number_ == correct_number; }

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

        if (number_ != " " && number_ != correct_number)
        {
            has_wrong_value = true;
            SetSquareColor(Color.red);
        }
    }

    public void SetCorrectNumber()
    {
        number_ = correct_number;
        SetNoteNumberValue(0);
        DisplayText();
    }

    void Start()
    {
        selected_ = false;
        note_active = false;

        SetNoteNumberValue(0);
    }

    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();

        foreach(var number in number_notes)
        {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;
    }

    private void SetClearEmptyNotes()
    {
        foreach(var number in number_notes)
        {
            if (number.GetComponent<Text>().text == "0")
                number.GetComponent<Text>().text = " ";
        }
    }

    private void SetNoteNumberValue(int value)
    {
        foreach(var number in number_notes)
        {
            if (value <= 0)
                number.GetComponent<Text>().text = null;
            else
                number.GetComponent<Text>().text = value.ToString();
        }
    }

    private void SetNoteSingleNumberValue(int value, bool force_update = false)
    {
        if(!note_active && !force_update)
            return;

        if (value <= 0)
            number_notes[value - 1].GetComponent<Text>().text = null;
        else
        {
            if (number_notes[value - 1].GetComponent<Text>().text == "" || force_update)
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
            else
                number_notes[value - 1].GetComponent<Text>().text = null;
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach(var note in notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }

    public void OnNotesActive(bool active)
    {
        note_active = active;
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

    private void DisplayText()
    {
        number_text.GetComponent<Text>().text = number_;
    }

    private bool SetNumber(string number)
    {
        string text_ = number_text.GetComponent<Text>().text; 
        int out_;
        
        if(text_ == " ")
        {
            number_text.GetComponent<Text>().text = number; // Buraki <> icindeki Text, UnityEngine.UI 'den cagrildi
            return true;
        }
        if (Int32.TryParse(text_, out out_))
        {
            if ((out_ >= 1 && out_ <= 9))
            {
                number_text.GetComponent<Text>().text = number; // Buraki <> icindeki Text, UnityEngine.UI 'den cagrildi
                return true;
            }
        }

        return false;
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
        GameEvents.OnNotesActive += OnNotesActive;
        GameEvents.OnClearNumber += OnClearNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnNotesActive -= OnNotesActive;
        GameEvents.OnClearNumber -= OnClearNumber;
    }

    public void OnClearNumber()
    {
        if(selected_ && !has_default_value)
        {
            number_ = null;
            has_wrong_value = false;
            SetSquareColor(Color.white);
            SetNoteNumberValue(0);
            DisplayText();
        }
    }

    public void OnSetNumber(int number)
    {
        bool changed; 
        if(selected_ && !has_default_value)
        {
            if (note_active && !has_wrong_value)
                SetNoteSingleNumberValue(number);
            else if(!note_active)
            {
                SetNoteNumberValue(0);
                changed = SetNumber(number.ToString());

                if (changed)
                {
                    if (number.ToString() != correct_number)
                    {
                        has_wrong_value = true;
                        var colors = this.colors;
                        colors.normalColor = Color.red;
                        this.colors = colors;

                        GameEvents.OnWrongNumberMethod();
                    }
                    else
                    {
                        has_wrong_value = false;
                        has_default_value = true;
                        var colors = this.colors;
                        colors.normalColor = Color.white;
                        this.colors = colors;
                    }
                }
            }
        }
    }

    public void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
            selected_ = false;
    }

    public void SetSquareColor(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }
}

