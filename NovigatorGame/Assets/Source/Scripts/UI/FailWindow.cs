using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailWindow : MonoBehaviour, IEntryPointSetupPlayer
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
        Cursor.lockState= CursorLockMode.None;
        Cursor.visible = true;
    } 
        
    
    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
    public void Setup(PlayerMovement player) => player.GetComponent<PlayerHealth>().OnDie += Open;
}
