using UnityEngine;
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
    private LoadingServicGame _loadingServic;

    [Inject]
    public void Constructor(SceneService sceneService, SaveService saveService, LoadingServicGame loadingServicGame)
    {
        _sceneService = sceneService;
        _saveService = saveService;
        _loadingServic = loadingServicGame;
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
        _loadingServic.Loaouding(false);
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
        _loadingServic.Loaouding(true);
        _saveService.LoadPosition();
        _sceneService.LoadScene("SampleScene");
    }
}