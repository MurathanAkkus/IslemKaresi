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

    public static void SaveBoardData(IkData.IkBoardData board_data, string level, int board_index, 
        int error_number, Dictionary<string, List<string>> grid_notes)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string current_time = "#time:" + Clock.GetCurrentTime();
        string level_string = "#level:" + level;
        string error_number_string = "#errors:" + error_number;
        string board_index_string = "#board_index:" + board_index.ToString();
        string unsolved_string = "#unsolved";
        string solved_string = "#solved";

        foreach(var unsolved_data in board_data.unsolved_data)
        {
            unsolved_string += unsolved_data.ToString() + ",";
        }

        foreach(var solved_data in board_data.solved_data)
        {
            solved_string += solved_data.ToString() + ",";
        }

        writer.WriteLine(current_time);
        writer.WriteLine(level_string);
        writer.WriteLine(error_number_string);
        writer.WriteLine(board_index);
        writer.WriteLine(unsolved_string);
        writer.WriteLine(solved_string);

        foreach(var square in grid_notes)
        {
            string square_string = "#" + square.Key + ":";
            bool save = false;

            foreach(var note in square.Value)
            {
                if(note != null)
                {
                    square_string += note + ",";
                    save = true;
                }
            }

            if (save)
                writer.WriteLine(square_string);
        }

        writer.Close();
    }
}
