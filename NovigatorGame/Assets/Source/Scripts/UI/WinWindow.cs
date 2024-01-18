using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour, IEntryPointTimerSetup
{
    [SerializeField] private GameObject _window;
    [SerializeField] private Button _restart;

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
    }

    public void Open()
    {
        _window.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void SetupTimer(Timer timer) => timer.OnEnd += Open;
}