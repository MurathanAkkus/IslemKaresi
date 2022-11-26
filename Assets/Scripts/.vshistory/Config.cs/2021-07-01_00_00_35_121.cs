using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Config : MonoBehaviour
{
    #if UNITY_ANDROID && !UNITY_EDITOR
        private static string dir = Application.persistentDataPath;
    #else
        private static string dir = Directory.GetCurrentDirectory();
#endif

    private static string file = @"\board_data.ini";
    static string path = dir + file;


    public static void DeleteDataFile()
    {
        File.Delete(path);
    }

    public static void SaveBoardData(IkData.IkBoardData boardData, string level, int board_index, 
        int error_number, Dictionary<string, List<string>> grid_notes)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
    }
}
