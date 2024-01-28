using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeMusic : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
            LoadVolume();
        else
            SetValueSound();    
    }

    public void SetValueSound()
    {
        float volume = _slider.value;
        _audioMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        _slider.value = PlayerPrefs.GetFloat("musicVolume");
        SetValueSound();
    }
}