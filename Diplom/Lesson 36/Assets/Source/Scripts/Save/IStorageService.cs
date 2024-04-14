using System;

public interface IStorageService
{
    public void Save<T>(T data, Action<bool> callback = null) where T : SaveData;

    public void Load<T>(string id, Action<T> callback);
}