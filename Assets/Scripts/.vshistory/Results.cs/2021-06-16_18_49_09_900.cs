using System;
using System.Data;
using UnityEngine;

public class Results : MonoBehaviour
{
    public void Result(string array)
    { 
        Evaluate(array).ToString();
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
