using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuService : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(Play);
        _quitButton.onClick.AddListener(Quit);
        _optionsButton.onClick.AddListener(Options);
        _backButton.onClick.AddListener(Back);
    }

    private void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void Options()
    {
        _menuPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    private void Back()
    {
        _menuPanel.SetActive(true);
        _optionsPanel.SetActive(false);
    }
}
