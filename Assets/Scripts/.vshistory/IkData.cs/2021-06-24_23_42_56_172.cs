//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class IkEasyData : MonoBehaviour
//{
//    public static List<IkData.IkBoardData> getData(List<string> solved)
//    {
//        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
        
//        data.Add(new IkData.IkBoardData(solved, "Easy"));
//        Debug.Log("test!");
//        return data;
//    }
//}

//public class IkMediumData : MonoBehaviour
//{
//    public static List<IkData.IkBoardData> getData(List<string> solved)
//    {
//        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
//        Debug.Log("test!!");
//        data.Add(new IkData.IkBoardData(solved, "Medium"));

//        return data;
//    }
//}

//public class IkHardData : MonoBehaviour
//{
//    public static List<IkData.IkBoardData> getData(List<string> solved)
//    {
//        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
//        Debug.Log("test!!!");
//        data.Add(new IkData.IkBoardData(solved, "Hard"));

//        return data;
//    }
//}

//public class IkVeryHardData : MonoBehaviour
//{
//    public static List<IkData.IkBoardData> getData(List<string> solved)
//    {
//        List<IkData.IkBoardData> data = new List<IkData.IkBoardData>();
//        Debug.Log("test!!!!");
//        data.Add(new IkData.IkBoardData(solved, "VeryHard"));

//        return data;
//    }
//}

//public class IkData : MonoBehaviour
//{
//    public static IkData Instance;

//    public struct IkBoardData
//    {
//        public string[] unsolved_data;
//        public string[] solved_data;

//        public IkBoardData(List<string> solved, string mode)
//        {
//            solved_data = solved.ToArray();
//            //unsolved_data = SetData(solved, mode);
//        }
//    }

//    public Dictionary<string, List<IkBoardData>> ik_game = new Dictionary<string, List<IkBoardData>>();

//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(this);
//    }

//    void Start()
//    {
        

//        ik_game.Add("Easy", IkEasyData.getData(obj.SetGridValues()));
//        ik_game.Add("Medium", IkMediumData.getData(obj.SetGridValues()));
//        ik_game.Add("Hard", IkHardData.getData(obj.SetGridValues()));
//        ik_game.Add("VeryHard", IkVeryHardData.getData(obj.SetGridValues()));
//    }
//}