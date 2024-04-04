using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(SpawnerEnemies))]
[RequireComponent(typeof(FactoryBonus))]
//[RequireComponent(typeof(ButtonsUI))]
public class EntryPoint : MonoBehaviour
{
    [Header("Start points position"), Space(5)] 
    [SerializeField] private Transform _pointPlayer;
    [SerializeField] private Transform _pointParallax;

    [SerializeField] private RectTransform _pointScoreUI;
    [SerializeField] private RectTransform _pointHealthUI;
    [SerializeField] private RectTransform _pointPauseAndPlayButtonsUI;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpawnerEnemies _spawnEnemy;

    private FactoryBonus _factoryBonus;
    private PlayerHealth _player;
    private PlayerHealth _createdPlayer;
    private ScoreUI _scoreUI;
    private ScoreUI _createdScoreUI;
    private HealthUI _healthUI;
    private HealthUI _createdHealthUI;
    private ButtonsUI _buttonsUI;
    private Button _pauseButton;
    private Button _createdPauseButton;
    private Button _playButton;
    private Button _createdPlayButton;
    private Parallax _parallax;
    private Parallax _createdParallax;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    [Inject]
    public void Constructor(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }
    
    private void Awake()
    {
        _spawnEnemy = GetComponent<SpawnerEnemies>();
        _factoryBonus = GetComponent<FactoryBonus>();
       // _buttonsUI = GetComponent<ButtonsUI>();
        _player = Resources.Load<PlayerHealth>("Player/Player");
        _scoreUI = Resources.Load<ScoreUI>("UI/Score");
        _healthUI = Resources.Load<HealthUI>("UI/Health");
        _pauseButton = Resources.Load<Button>("UI/PauseButton");
        _playButton = Resources.Load<Button>("UI/PlayButton");
        _parallax = Resources.Load<Parallax>("UI/Parallax");
    }

    private void OnEnable()
    {
        CreatePlayer();
        CreateScoreUI();
        CreateHealthUI();
        CreatedPauseButtonUI();
        CreatedPlayButtonUI();
        CreateParallax();
        _buttonsUI.Setup(_createdPauseButton, _createdPlayButton);
        //_spawnEnemy.Setup(_buttonsUI);
        _spawnEnemy.Setup(_createdPlayer);
        _spawnEnemy.Setup(_createdScoreUI);
        _spawnEnemy.Setup(_factoryBonus);
        _factoryBonus.Setup(_buttonsUI);
    }

    private void CreateScoreUI()
    {
        _createdScoreUI = Instantiate(_scoreUI, _pointScoreUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
    }

    private void CreateHealthUI()
    {
        _createdHealthUI = Instantiate(_healthUI, _pointHealthUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
        _createdHealthUI.Setup(_createdPlayer);
    }

    private void CreatedPauseButtonUI()
    {
        _createdPauseButton = Instantiate(_pauseButton,
            _pointPauseAndPlayButtonsUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
    }

    private void CreatedPlayButtonUI()
    {
        _createdPlayButton = Instantiate(_playButton,
            _pointPauseAndPlayButtonsUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
    }

    private void CreatePlayer()
    {
        _createdPlayer = _diContainer
            .InstantiatePrefab(_player, _pointPlayer.transform.position, Quaternion.identity, null)
            .GetComponent<PlayerHealth>();
        _createdPlayer.Constructor(_buttonsUI);
    }
    
    private void CreateParallax()
    {
        _createdParallax = Instantiate(_parallax, _pointParallax.transform.position, Quaternion.identity);
        _createdParallax.Setup(_buttonsUI);
    }
}