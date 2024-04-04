using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    private float _volumeMixer;
    private IStorageService _storageService;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();
    }

    private void Start()
    {
        LoadSettings();
    }

    private void Update()
    {
        SetValue();
    }

    public void SetValue()
    {
        _volumeMixer = (_slider.value * 100) - 80;
        _audioMixer.SetFloat("MasterVolume", _volumeMixer);
        SaveSettings(_volumeMixer);
    }

    private void SaveSettings(float volume)
    {
        AudioSettingsSaveData saveData = new AudioSettingsSaveData("VolumeMixer", volume);
        _storageService.Save(saveData);
    }

    private void LoadSettings()
    {
        _storageService.Load("VolumeMixer", (AudioSettingsSaveData saveData) =>
        {
            if (saveData != null)
            {
                _slider.value = (saveData.Volume + 80) / 100;
            }
        });
    }
}

public class AudioSettingsSaveData : SaveData
{
    public AudioSettingsSaveData(string id, float volume) : base(id, typeof(AudioSettingsSaveData).FullName)
    {
        Volume = volume;
    }

    public float Volume { get; private set; }
}