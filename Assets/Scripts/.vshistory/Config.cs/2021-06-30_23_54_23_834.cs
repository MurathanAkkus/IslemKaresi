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
}
