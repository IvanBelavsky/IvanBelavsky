using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(FactoryEnemy))]
[RequireComponent(typeof(FactoryAmmo))]
[RequireComponent(typeof(FactoryBonus))]
public class SaveService : MonoBehaviour
{
    public event Action OnSave;

    private List<ISavePositionble> _position = new List<ISavePositionble>();
    private List<ISavePositionble> _saveDatas = new List<ISavePositionble>();
    private FactoryEnemy _factoryEnemy;
    private FactoryAmmo _factoryAmmo;
    private FactoryBonus _factoryBonus;
    private ScoreUI _scoreUI;
    private HealthUI _healthUI;
    private LoadingServicGame _loadingServic;
    private PlayerHealth _playerHealth;
    private string _filePath;

    [Inject]
    public void Constructor(LoadingServicGame loadingServicGame)
    {
        _loadingServic = loadingServicGame;
    }

    public void Setup(ScoreUI scoreUI)
    {
        _scoreUI = scoreUI;
    }

    public void Setup(HealthUI healthUI)
    {
        _healthUI = healthUI;
    }

    public void Setup(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    [field: SerializeField] public bool IsLoad { get; private set; }
    public FileSaveData CurrentSaveData { get; private set; }

    private void Awake()
    {
        _factoryEnemy = GetComponent<FactoryEnemy>();
        _factoryAmmo = GetComponent<FactoryAmmo>();
        _factoryBonus = GetComponent<FactoryBonus>();
        _filePath = Application.persistentDataPath + "/SaveData.data";

        if (IsFileExist())
            CurrentSaveData = Load();
        else
            CurrentSaveData = new FileSaveData();
    }

    private void Start()
    {
        if (_loadingServic.IsLoad)
        {
            LoadPosition();
            _loadingServic.Loaouding(false);
        }
        else
            ClearSaveData();
    }

    public void AddPosition(ISavePositionble savePosition)
    {
        _position.Add(savePosition);
    }

    public void RemovePosition(ISavePositionble position)
    {
        _position.Remove(position);
    }

    public void SaveObjects()
    {
        ClearSaveData();
        _scoreUI.SaveScore();
        _healthUI.SaveHealth();
        foreach (ISavePositionble saveData in _position)
        {
            saveData.SavePosition();
            Debug.Log(saveData);
        }
        IsLoad = true;
        _loadingServic.Loaouding(true);
    }

    public void LoadPosition()
    {
        Dictionary<string, SaveDatas> bufferData = new Dictionary<string, SaveDatas>(CurrentSaveData.Datas);
        foreach (SaveDatas saveDatas in bufferData.Values)
        {
            if (saveDatas.Type == typeof(EnemyHealth))
            {
                EnemySaveData enemySaveData = (EnemySaveData)saveDatas;
                EnemyHealth enemyHealth = _factoryEnemy.CreateEnemyRed(enemySaveData.EnemyPosition.Vector())
                    .GetComponent<EnemyHealth>();
                enemyHealth.Setup(saveDatas.Id);
                enemyHealth.LoadPosition();
                if (_scoreUI != null)
                    _scoreUI.AddEnemy(enemyHealth);
            }

            if (saveDatas.Type == typeof(AmmoBasic))
            {
                AmmoSaveData ammoSaveData = (AmmoSaveData)saveDatas;
                AmmoBasic ammo;
                if (ammoSaveData.AmmoType == AmmoType.playerType)
                {
                    ammo = _factoryAmmo.CreatedPlayerAmmo(ammoSaveData.Position.Vector())
                        .GetComponent<AmmoPlayer>();
                }
                else
                {
                    ammo = _factoryAmmo.CreatedEnemyAmmo(ammoSaveData.Position.Vector())
                        .GetComponent<EnemyAmmo>();
                }

                ammo.Setup(saveDatas.Id);
                ammo.LoadPosition();
            }

            if (saveDatas.Type == typeof(ScoreUI))
            {
                if (_scoreUI != null)
                {
                    _scoreUI.Setup(saveDatas.Id);
                    _scoreUI.LoadScore();
                }
            }

            if (saveDatas.Type == typeof(HealthUI))
            {
                if (_healthUI != null)
                {
                    _healthUI.Setup(saveDatas.Id);
                    _healthUI.LoadHealth();
                }
            }

            if (saveDatas.Type == typeof(PlayerHealth))
            {
                if (_playerHealth != null)
                {
                    _playerHealth.Setup(saveDatas.Id);
                    _playerHealth.LoadPosition();
                }
            }

            if (saveDatas.Type == typeof(Bonus))
            {
                BonusSaveData ammoSaveData = (BonusSaveData)saveDatas;
                Bonus bonus = _factoryBonus.CreateBonus(ammoSaveData.BonusPosition.Vector()).GetComponent<Bonus>();
                bonus.Setup(saveDatas.Id);
                bonus.LoadPosition();
            }
        }
        IsLoad = false;
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

    public void ClearSaveData()
    {
        CurrentSaveData.ClearData();
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

    public void ClearData()
    {
        Datas.Clear();
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