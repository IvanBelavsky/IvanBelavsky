using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;

    private SaveService _saveService;
    private float _volumeMixer;

    [Inject]
    public void Constructor(SaveService saveService)
    {
        _saveService = saveService;
    }

    [field: SerializeField] public string ID { get; private set; } = "Volume";

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

    public void SaveSettings(float volume)
    {
        PlayerPrefs.SetFloat(ID, volume);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(ID))
        {
            float savedVolume = PlayerPrefs.GetFloat(ID);
            _slider.value = (savedVolume + 80) / 100; 
            SetValue(_slider.value);
        }
        else
        {
            _slider.value = 1.0f;
            SetValue(_slider.value);
        }
    }
}
