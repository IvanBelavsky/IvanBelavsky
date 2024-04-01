using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsUI : MonoBehaviour
{
    public event Action OnClickPauseButton; 
    public event Action OnClickPlayButton; 
    
    [SerializeField] private Button _pause;
    [SerializeField] private Button _play;

    private void Start()
    {
        _pause.onClick.AddListener(ClickPause);
        _play.onClick.AddListener(ClickPlay);
    }

    public void Setup(Button pause, Button play)
    {
        _pause = pause;
        _play = play;
    }

    private void ClickPause()
    {
        _pause.gameObject.SetActive(false);
        _play.gameObject.SetActive(true);
        OnClickPauseButton?.Invoke();
    }

    private void ClickPlay()
    {
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        OnClickPlayButton?.Invoke();
    }
}
