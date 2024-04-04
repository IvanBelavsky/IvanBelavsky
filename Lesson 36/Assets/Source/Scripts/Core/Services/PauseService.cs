using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseService : MonoBehaviour
{
    private List<IPauseble> _pauses = new List<IPauseble>();
    private ButtonsUI _buttonsUI;

    [Inject]
    public void Constructor(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }

    private void OnEnable()
    {
        _buttonsUI.OnClickPauseButton += Pause;
        _buttonsUI.OnClickPlayButton += Continue;
    }

    private void OnDisable()
    {
        _buttonsUI.OnClickPauseButton -= Pause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void AddPauses(IPauseble pauseble)
    {
        _pauses.Add(pauseble);
    }

    public void RemovePauses(IPauseble pauseble)
    {
        _pauses.Remove(pauseble);
    }

    private void Pause()
    {
        foreach (IPauseble pause in _pauses)
        {
            pause.PlayPause();
        }
    } 
    
    private void Continue()
    {
        foreach (IPauseble pause in _pauses)
        {
            pause.Continue();
        }
    }
    
}
