using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkEasyData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        
        data.Add(new IkData.IkBoardData(solved, "Easy"));
        Debug.Log("test!");
        return data;
    }
}

public class IkMediumData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        Debug.Log("test!!");
        data.Add(new IkData.IkBoardData(solved, "Medium"));

        return data;
    }
}

public class IkHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        Debug.Log("test!!!");
        data.Add(new IkData.IkBoardData(solved, "Hard"));

        return data;
    }
}

public class IkVeryHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData(List<string> solved)
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        Debug.Log("test!!!!");
        data.Add(new IkData.IkBoardData(solved, "VeryHard"));

        return data;
    }
}

public class IkData : MonoBehaviour
{
    public static IkData Instance;

    public static string[] SetData(List<string> solved, string mode)
    {
        
        string[] unsolved_data = solved.ToArray();
        //Debug.Log("Ne oldu"); 
        Debug.Log(unsolved_data[1]);
 
        for (int j = 0; j < 29; j++)
        {
            Debug.Log(solved[j]);
            if (j == 5 || j == 17)
            {   // atlama yapýp, bir arttýrýyor
                j += 6;
                continue;
            }
            if (int.Parse(solved[j]) >= 1 && int.Parse(solved[j]) <= 9)
                unsolved_data[j] = "0";
        }
        
        List<int> numbers = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        int i = 0, rand, idx;

        if (mode.Equals("Easy"))
            i = 3;  // 3 ipucu verecek

        else if (mode.Equals("Medium"))
            i = 2;  // 2 ipucu verecek

        else if (mode.Equals("Hard"))
            i = 1;  // 1 ipucu verecek
                    // i = 0 geliyorsa, VeryHard modda oynuyor demektir, ipucu verilmeyecek
        if (i != 0)
        {
            do
            {
                rand = Random.Range(0, numbers.Count - 1);
                idx = solved.IndexOf(numbers[rand].ToString());  // rastgele üretilen index'teki sayinin
                numbers.RemoveAt(rand); // tablodaki yerini bulup, onun index'ini aliyor.
                unsolved_data[idx] = solved[idx]; // Oradaki index'teki sayiyi aciyor kullaniciya
                i--;
            } while (i > 0);
        }
        return unsolved_data;
    }

    public struct IkBoardData
    {
        public string[] unsolved_data;
        public string[] solved_data;

        public IkBoardData(List<string> solved, string mode)
        {
            solved_data = solved.ToArray();
            unsolved_data = SetData(solved, mode);
        }
    }

    public Dictionary<string, List<IkBoardData>> ik_game = new Dictionary<string, List<IkBoardData>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        IkGrid obj = new IkGrid();

        ik_game.Add("Easy", IkEasyData.getData(obj.SetGridValues()));
        ik_game.Add("Medium", IkMediumData.getData(obj.SetGridValues()));
        ik_game.Add("Hard", IkHardData.getData(obj.SetGridValues()));
        ik_game.Add("VeryHard", IkVeryHardData.getData(obj.SetGridValues()));
    }
}