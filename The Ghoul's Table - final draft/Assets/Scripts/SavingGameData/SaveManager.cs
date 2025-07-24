using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/blackjack.json";
    
    public static void Save (BlackjackData data)
    {
        string jsonData = JsonUtility.ToJson(data); //converting to json data 
        File.WriteAllText(path, jsonData); //writing to the path
        Debug.Log("written to " + path);
    }

    public static BlackjackData Load()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            BlackjackData data = JsonUtility.FromJson<BlackjackData>(jsonData); //converting from jsondata to blackjackdata type
            Debug.Log("data loaded from " + path);
            return data;
        }
        else
        {
            Debug.Log("no data saved");
            return null;
        }
    }

    public static void UpdateKilled ()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            BlackjackData data = JsonUtility.FromJson<BlackjackData>(jsonData);
            data.hasBeenKilled = false;
            jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(path, jsonData); //updating killed. this should only be called in shooting scene. 
        }
    }

    public static void DeleteSaved()
    {
        if (File.Exists(path))
        { 
            File.Delete(path); 
        }
    }
}
