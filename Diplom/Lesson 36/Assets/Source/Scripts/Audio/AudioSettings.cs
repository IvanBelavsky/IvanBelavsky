using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private SaveService _saveService;

    private float _volumeMixer;

    [Inject]
    public void Constructor(SaveService saveService)
    {
        _saveService = saveService;
    }

    [field: SerializeField] public string ID = "Volume";
    
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetValue);
    }

    private void Start()
    {
        LoadSettings();
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetValue);
    }

    public void SetValue(float value)
    {
        _volumeMixer = (value * 100) - 80;
        _audioMixer.SetFloat("MasterVolume", _volumeMixer);
        SaveSettings(_volumeMixer);
    }

    private void SaveSettings(float volume)
    {
        _saveService.CurrentSaveData.AddData(ID, new AudioSettingsSaveData(volume, ID, typeof(AudioSettings)));
        _saveService.Save();
    }

    private void LoadSettings()
    {
        if (_saveService.CurrentSaveData.TryGetData<AudioSettingsSaveData>(ID,
                out AudioSettingsSaveData volumeSaveData))
        {
            _slider.value = (volumeSaveData.Volume + 80) / 100;
            _volumeMixer = volumeSaveData.Volume;
        }
        else
        {
            _volumeMixer = 20;
        }
    }
}

[Serializable]
public class AudioSettingsSaveData : SaveDatas
{
    public AudioSettingsSaveData(float volume, string id, Type type) : base(id, type)
    {
        Volume = volume;
    }

    public float Volume { get; private set; }
}