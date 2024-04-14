using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveService : MonoBehaviour
{
    public Action OnSave;

    private List<ISavePositionble> _position = new List<ISavePositionble>();
    private string _filePath;

    public FileSaveData CurrentSaveData { get; private set; }

    private void Awake()
    {
        _filePath = Application.persistentDataPath + "/SaveData.data";

        if (IsFileExist())
            CurrentSaveData = Load();
        else
            CurrentSaveData = new FileSaveData();
    }

    public void AddPosition(ISavePositionble savePosition)
    {
        _position.Add(savePosition);
    }

    public void RemovePosition(ISavePositionble position)
    {
        _position.Remove(position);
    }

    public void SavePosition()
    {
        foreach (ISavePositionble position in _position.ToList())
        {
            position.SavePosition();
        }
    }

    private void LoadPosition()
    {
        foreach (ISavePositionble position in _position.ToList())
        {
            position.LoadPosition();
        }
    }

    public void LoadSceneWithSavedPositions(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        LoadPosition();
    }

    public void Save()
    {
        OnSave?.Invoke();
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, CurrentSaveData);
        }
    }

    public FileSaveData Load()
    {
        FileSaveData returnObj = new FileSaveData();
        if (IsFileExist())
        {
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                returnObj = (FileSaveData)loadedData;
            }
        }
        else
        {
            Save();
        }

        return returnObj;
    }


    public bool IsFileExist()
    {
        if (File.Exists(_filePath))
            return true;
        return false;
    }
}

[Serializable]
public class FileSaveData : SaveDatas
{
    public Language Language { get; private set; }
    public Dictionary<string, SaveDatas> Datas = new Dictionary<string, SaveDatas>();

    public FileSaveData()
    {
    }

    public void UpdateLanguage(Language language)
    {
        Language = language;
    }

    public bool TryGetData<T>(string id, out T data) where T : SaveDatas
    {
        foreach (SaveDatas saveData in Datas.Values)
        {
            if (saveData.Id == id)
            {
                data = (T)saveData;
                return true;
            }
        }

        data = null;
        return false;
    }

    public void AddData(string id, SaveDatas saveData)
    {
        Dictionary<string, SaveDatas> datasBuffer = new Dictionary<string, SaveDatas>(Datas);
        foreach (string datasKey in datasBuffer.Keys)
        {
            if (datasKey == id)
            {
                Datas.Remove(id);
            }
        }

        Datas.Add(id, saveData);
    }

    public void RemoveData(string id)
    {
        Dictionary<string, SaveDatas> datasBuffer = new Dictionary<string, SaveDatas>(Datas);
        foreach (string datasKey in datasBuffer.Keys)
        {
            if (datasKey == id)
            {
                Datas.Remove(id);
            }
        }
    }
}

[Serializable]
public class SaveDatas
{
    public string Id;
    public Type Type;

    public SaveDatas()
    {
    }

    public SaveDatas(string id, Type type)
    {
        Id = id;
        Type = type;
    }
}

[Serializable]
public struct VectorSerializable
{
    public VectorSerializable(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public VectorSerializable(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }

    public Vector3 Vector()
    {
        return new Vector3(X, Y, Z);
    }

    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
}