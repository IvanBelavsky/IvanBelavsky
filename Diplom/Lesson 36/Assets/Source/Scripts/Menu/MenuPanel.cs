using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _continueButton;

    private SceneService _sceneService;
    private SaveService _saveService;

    [Inject]
    public void Constructor(SceneService sceneService, SaveService saveService)
    {
        _sceneService = sceneService;
        _saveService = saveService;
    }

    private void Awake()
    {
        _playButton.onClick.AddListener(Play);
        _quitButton.onClick.AddListener(Quit);
        _optionsButton.onClick.AddListener(OpenOptions);
        _backButton.onClick.AddListener(CloseBack);
        _continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Play);
        _quitButton.onClick.RemoveListener(Quit);
        _optionsButton.onClick.RemoveListener(OpenOptions);
        _backButton.onClick.RemoveListener(CloseBack);
        _continueButton.onClick.RemoveListener(Continue);
    }

    private void Play()
    {
        _sceneService.LoadScene("SampleScene");
    }

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void OpenOptions()
    {
        _menuPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    private void CloseBack()
    {
        _menuPanel.SetActive(true);
        _optionsPanel.SetActive(false);
    }

    private void Continue()
    {
        _saveService.LoadSceneWithSavedPositions("SampleScene");
    }
}