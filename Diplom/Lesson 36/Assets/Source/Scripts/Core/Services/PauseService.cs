using System.Collections.Generic;
using System.Linq;
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

   public void Pause()
    {
        foreach (IPauseble pause in _pauses.ToList())
        {
            pause.PlayPause();
        }
    } 
    
   public void Continue()
    {
        foreach (IPauseble pause in _pauses.ToList())
        {
            pause.Continue();
        }
    }
}
