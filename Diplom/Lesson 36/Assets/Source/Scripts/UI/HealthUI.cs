using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslate))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] _healthUI;

    private PlayerHealth _player;
    private TextMeshProUGUI _text;
    private SaveService _saveService;
    private GameBehaviourUI _mainMenu;
    private TextTranslate _textTranslate;

    [Inject]
    public void Constructor(PlayerHealth player, SaveService saveService, GameBehaviourUI mainMenu)
    {
        _player = player;
        _saveService = saveService;
        _mainMenu = mainMenu;
        if (_player != null)
            _player.OnHealthChange += HealthUpdate;
    }
    
    public void Setup(string id)
    {
        ID = id;
    }
    
    [field: SerializeField] public string ID = "HealthUI";

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textTranslate = GetComponent<TextTranslate>();
        _saveService.Setup(this);
    }

    private void OnEnable()
    {
        if (_mainMenu != null)
            _mainMenu.OnClickMainMenuButton += SaveHealth;
    }

    private void Start()
    {
        _player.OnHealthChange += HealthUpdate;
    }

    private void OnDisable()
    {
        _player.OnHealthChange -= HealthUpdate;
        if (_mainMenu != null)
            _mainMenu.OnClickMainMenuButton -= SaveHealth;
    }
    
    public void SaveHealth()
    {
        _saveService.CurrentSaveData.AddData(ID, new HealthSaveData(_player.Health, ID, typeof(HealthUI)));
        _saveService.Save();
        Debug.Log("Save");
    }
    
    public void LoadHealth()
    {
        if (_saveService.CurrentSaveData.TryGetData<HealthSaveData>(ID, out HealthSaveData healthSaveData))
        {
            _player.SetHealth(healthSaveData.HealthUI);
            HealthUpdate();
        }
    }

    private void HealthUpdate()
    {
        for (int i = 0; i < _healthUI.Length; i++)
        {
            if (i < _player.Health)
            {
                _healthUI[i].enabled = true;
            }
            else
            {
                _healthUI[i].enabled = false;
            }
        }
    }
}

[Serializable]
public class HealthSaveData : SaveDatas
{
    public HealthSaveData(float healthUI, string id, Type type) : base(id, type)
    {
        HealthUI = healthUI;
    }

    public float HealthUI { get; private set; }
}