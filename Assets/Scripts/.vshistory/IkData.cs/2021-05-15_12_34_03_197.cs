using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IkEasyData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData()
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36]
            ));
        return data;
    }
}

public class IkMediumData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData()
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36], new int[36]
            ));
        return data;
    }
}

public class IkHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData()
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36], new int[36]
            ));
        return data;
    }
}

public class IkVeryHardData : MonoBehaviour
{
    public static List<IkData.IkBoardData> getData()
    {
        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();

        data.Add(new IkData.IkBoardData(
            new int[36], new int[36]
            ));
        return data;
    }
}

public class IkData : MonoBehaviour
{
    public static IkData Instance;
    private GameObject grid_square; // Grid'in bir karesi icin degisken
    private List<GameObject> grid_squares_ = new List<GameObject>(); // Grid'in karelerinin listesi

    public struct IkBoardData
    {
        public int[] unsolved_data;
        public int[] solved_data;

        public IkBoardData(int[] solved) : this()
        {
            this.unsolved_data = solved;
            this.solved_data = solved;
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
        Solved();
        ik_game.Add("Easy", IkEasyData.getData());
        ik_game.Add("Medium", IkMediumData.getData());
        ik_game.Add("Hard", IkHardData.getData());
        ik_game.Add("VeryHard", IkVeryHardData.getData());
    }


}