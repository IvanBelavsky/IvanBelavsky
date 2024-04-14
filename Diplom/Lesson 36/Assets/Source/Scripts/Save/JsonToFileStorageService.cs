using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonToFileStorageService : IStorageService
{
    public void Save<T>(T data, Action<bool> callback = null) where T : SaveData
    { 
        string path = BildPath(data.ID);

        string json = JsonConvert.SerializeObject(data);

        using (StreamWriter fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }

        callback?.Invoke(true);
    }

    public void Load<T>(string id, Action<T> callback)
    {
        string path = BildPath(id);

        using (StreamReader fileReader = new StreamReader(path))
        {
            string json = fileReader.ReadToEnd();
            T data = JsonConvert.DeserializeObject<T>(json);
            callback.Invoke(data);
        }
    }

    private string BildPath(string id)
    {
        return Path.Combine(Application.persistentDataPath, id);
    }
}