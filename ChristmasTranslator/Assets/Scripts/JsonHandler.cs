using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonHandler
{
    private static string ReadJsonFile()
    {
        string filePath = Application.streamingAssetsPath + "/data.json";

        if (File.Exists(filePath))
            return File.ReadAllText(filePath);
        return "";
    }
    
    public static RootObject GetLanguageObject()
    {
        string json = ReadJsonFile();
        return JsonUtility.FromJson<RootObject>(json);
    }
}
