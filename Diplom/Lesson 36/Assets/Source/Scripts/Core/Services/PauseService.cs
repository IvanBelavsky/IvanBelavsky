using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PauseService : MonoBehaviour
{
    private List<IPauseble> _pauses = new List<IPauseble>();
    private GameBehaviourUI _gameBehaviourUI;

    [Inject]
    public void Constructor(GameBehaviourUI gameBehaviourUI)
    {
        _gameBehaviourUI = gameBehaviourUI;
    }

    private void OnEnable()
    {
        _gameBehaviourUI.OnClickPauseButton += Pause;
        _gameBehaviourUI.OnClickPlayButton += Continue;
    }

    private void OnDisable()
    {
        _gameBehaviourUI.OnClickPauseButton -= Pause;
        _gameBehaviourUI.OnClickPlayButton -= Continue;
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
        foreach (IPauseble pause in _pauses.ToList())
        {
            pause.PlayPause();
        }
    } 
    
    private void Continue()
    {
        foreach (IPauseble pause in _pauses.ToList())
        {
            pause.Continue();
        }
    }
}
