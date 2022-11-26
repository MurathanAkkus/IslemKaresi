using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public struct IkBoardData
    {
        public string[] unsolved_data;
        public string[] solved_data;

        public IkBoardData(List<string> solved, string mode)
        {
            solved_data = solved.ToArray();
            //unsolved_data = SetData(solved, mode);
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
        ik_game.Add("Easy", IkEasyData.getData(obj.SetGridValues()));
        ik_game.Add("Medium", IkMediumData.getData(obj.SetGridValues()));
        ik_game.Add("Hard", IkHardData.getData(obj.SetGridValues()));
        ik_game.Add("VeryHard", IkVeryHardData.getData(obj.SetGridValues()));
    }

    private string[] SetData(List<string> solved, string[] unsolved, string mode)
    {
        string[] unsolved = solved.ToArray();
        int out_;
        for (int j = 0; j < 29; j++)
        {
            if (j == 5 || j == 17)
                continue;
            if (Int32.TryParse(solved[j], out out_))
                if (out_ >= 1 && out_ <= 9)
                    unsolved[j] = null;
        }

        List<string> numbers = new List<string>(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
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
                idx = solved.IndexOf(numbers[rand]);  // rastgele üretilen index'teki sayinin
                numbers.RemoveAt(rand); // tablodaki yerini bulup, onun index'ini aliyor.
                unsolved[idx] = solved[idx]; // Oradaki index'teki sayiyi aciyor kullaniciya
                i--;
            } while (i > 0);
        }
        return unsolved;
    }
}