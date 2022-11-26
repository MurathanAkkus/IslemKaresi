using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class Config : MonoBehaviour
{
    #if UNITY_ANDROID && !UNITY_EDITOR
        private static string dir = Application.persistentDataPath;
    #else
        private static string dir = Directory.GetCurrentDirectory();
    #endif

    private static string file = @"\board_data.ini";
    private static string path = dir + file;


    public static void DeleteDataFile()
    {
        File.Delete(path);
    }

    public static void SaveBoardData(IkData.IkBoardData board_data, string level, int board_index,
        int error_number, Dictionary<string, List<string>> grid_notes, int board_max)
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);
        string current_time = "#time:" + Clock.GetCurrentTime();
        string level_string = "#level:" + level;
        string error_number_string = "#errors:" + error_number;
        string board_index_string = "#board_index:" + board_index.ToString();
        string unsolved_string = "#unsolved:";
        string solved_string = "#solved:";

        foreach (var unsolved_data in board_data.unsolved_data)
        {
            //if (unsolved_string.Length == )
                unsolved_string += unsolved_data;
            unsolved_string += unsolved_data + ",";
            Debug.Log(unsolved_string.Length);
        }
            
        
        foreach (var solved_data in board_data.solved_data)
            solved_string += solved_data + ",";
        
        writer.WriteLine(current_time);
        writer.WriteLine(level_string);
        writer.WriteLine(error_number_string);
        writer.WriteLine(board_index_string);
        writer.WriteLine(unsolved_string);
        writer.WriteLine(solved_string);
        writer.WriteLine(board_max);
        foreach (var square in grid_notes)
        {
            string square_string = "#" + square.Key + ":";
            bool save = false;

            foreach (var note in square.Value)
            {
                if (note != "" && note != " ")
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

    public static Dictionary<int, List<int>> GetGridNotes()
    {
        Dictionary<int, List<int>> grid_notes = new Dictionary<int, List<int>>();
        string line;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if(word[0].Equals("#square_note"))
            {
                int square_index;
                List<int> notes = new List<int>();
                int.TryParse(word[1], out square_index);

                string[] substring = Regex.Split(word[2], ",");

                foreach(var note in substring)
                {
                    int note_number;
                    int.TryParse(note, out note_number);
                    if (note_number > 0)
                        notes.Add(note_number);
                }

                grid_notes.Add(square_index, notes);
            }
        }
        file.Close();
        return grid_notes;
    }

    public static string ReadBoardLevel()
    {
        string line;
        string level = null;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0].Equals("#level"))
                level = word[1];
        }
        file.Close();
        return level;
    }


    public static IkData.IkBoardData ReadGridData()
    {
        string line;
        StreamReader file = new StreamReader(path);

        string[] unsolved_data = new string[36];
        string[] solved_data = new string[36];

        int unsolved_index = 0;
        int solved_index = 0;

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if(word[0].Equals("#unsolved"))
            {
                string[] substring = Regex.Split(word[1], ",");
                foreach(string value in substring)
                {
                    Debug.Log("test " + value);
                    
                    
                        unsolved_data[unsolved_index] = value;
                        unsolved_index++;
                    
                }
            }
            if(word[0].Equals("#solved"))
            {
                string[] substring = Regex.Split(word[1], ",");

                foreach(var value in substring)
                {
                    solved_data[solved_index] = value;
                    solved_index++;
                }
            }
        }
        file.Close();
        return new IkData.IkBoardData(solved_data, unsolved_data);
    }

    public static int ReadGameBoardLevel()
    {
        int level = -1;
        string line;
        StreamReader file = new StreamReader(path);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if(word[0].Equals("#board_index"))
                int.TryParse(word[1], out level);
        }
        file.Close();
        return level;
    }

    public static float ReadGameTime()
    {
        float time = -1.0f;
        string line;

        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0].Equals("#time"))
                float.TryParse(word[1], out time);
        }
        file.Close();
        return time;
    }

    public static int ErrorNumber()
    {
        int errors = 0;
        string line;

        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0].Equals("#errors"))
                int.TryParse(word[1], out errors);
        }
        file.Close();
        return errors;
    }

    public static bool GameDataFileExist() { return File.Exists(path); }
}
