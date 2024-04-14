using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneService : MonoBehaviour
{
    private PanelFade _fadePanel;

    [Inject]
    public void Constructor(PanelFade fadePanel)
    {
        _fadePanel = fadePanel;
    }

    public void LoadScene(string name)
    {
        _fadePanel.FadeIn((() => SceneManager.LoadScene(name)));
    }

    public void Restart()
    {
        _fadePanel.FadeIn(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }
}
