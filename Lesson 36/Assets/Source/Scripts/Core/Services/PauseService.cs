using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseService : MonoBehaviour
{
    private List<IPauseble> _pauses = new List<IPauseble>();

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
