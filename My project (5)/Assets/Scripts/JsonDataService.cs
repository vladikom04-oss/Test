using System.IO;
using UnityEngine;

public interface IDataService
{
    void SaveData<T>(T data);
    T LoadData<T>();
}

public class JsonDataService : IDataService
{
    private string GetFilePath(string fileName = "gamesave.json")
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public void SaveData<T>(T data) 
    {
        string filePath = GetFilePath();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public T LoadData<T>()
    {
        string filePath = GetFilePath();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        return default (T);
    }
}
