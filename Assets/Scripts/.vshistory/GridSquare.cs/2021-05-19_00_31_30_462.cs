using UnityEngine;
using UnityEngine.UI;

//Grid'in icindeki bir karenin class'i
public class GridSquare : Selectable // UnityEngine.UI class'indan Selectable API'sini cagirarak,
{                                    // basit secilebilir nesne ve kontroller kullanilabilinir.
    public GameObject number_text;
    private string number_ = "0";
    
    private void DisplayText()
    {
        if (int.Parse(number_) == 0)
            number_text.GetComponent<Text>().text = null;
        else if (int.Parse(number_) > 0 && int.Parse(number_) <= 9) 
            number_text.GetComponent<Text>().text = number_; // Buraki <> icindeki Text, UnityEngine.UI 'den cagrildi
    }

    public void SetNumber(string number)
    {   // Gelen rakami degistirip,
        // ekrana yazdir
        number_ = number;
        DisplayText();
    }

    public void SetOpt()
    {
        string[] opt = { "+", "-", "*", "/" };
        int number = Random.Range(0, 4);
        number_text.GetComponent<Text>().text = opt[number];
    }
    public void SetOpt_withoutDiv()
    {
        string[] opt = { "+", "-", "*" };
        int number = Random.Range(0, 3);
        number_text.GetComponent<Text>().text = opt[number];
    }
}
