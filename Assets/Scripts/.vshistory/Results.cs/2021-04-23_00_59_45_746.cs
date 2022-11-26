using System;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public GameObject number_text;
    private string str;

    private void DisplayText()
    {
        number_text.GetComponent<Text>().text = Evaluate(str).ToString();
    }

    public void Result(string array)
    {
        str = array;
        DisplayText();
    }

    private static int Evaluate(string expression)
    {
        var loDataTable = new DataTable();
        var loDataColumn = new DataColumn("Eval", typeof(double), expression);
        loDataTable.Columns.Add(loDataColumn);
        loDataTable.Rows.Add(0);
        return Convert.ToInt32(loDataTable.Rows[0]["Eval"]);
    }

    //internal void Result(string[] y_array)
    //{
    //    throw new NotImplementedException();
    //}
}
