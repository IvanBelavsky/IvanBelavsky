using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(FabricaEnemy))]
public class EntryPoint : MonoBehaviour
{
    [Space(10), Header("Parents")] [SerializeField]
    private Transform _canvas;

    [Space(10), Header("Start points")] [SerializeField]
    private Transform _playerStartPlayer;

    [SerializeField] private List<Transform> _enemyStartPoint = new List<Transform>();
    [SerializeField] private List<Transform> _enemyFastStartPoint = new List<Transform>();
    [SerializeField] private List<Transform> _startPoinBonusKillEnemy = new List<Transform>();
    [SerializeField] private List<Transform> _startPointBonusHealth = new List<Transform>();

    private Enemy _basicEnemy;
    private Enemy _fastEnemy;
    private Enemy _createEnemy;
    private Enemy _createFastEnemy;
    private FabricaEnemy _fabrica;
    private PlayerMovement _player;
    private PlayerMovement _createdPlayer;
    private PlayerHealth _playerHealth;
    private Bonus _bonus;
    private Bonus _createdBonus;
    private BonusHealth _bonusHealth;
    private BonusHealth _bonusHealthCreated;
    private Timer _timer;
    private Timer _timerCreated;
    private WinWindow _winWindow;
    private FailWindow _failWindow;
    //private TimeTickBonus _tickBonus;
    private Coroutine _respawnTick;
    private List<IEntryPointSetupPlayer> _setupPlayers = new List<IEntryPointSetupPlayer>();
    private List<IEntryPointTimerSetup> _setupTimer = new List<IEntryPointTimerSetup>();
    //private List<IEntryPointSetupPlayer> _setupBonusTimer = new List<IEntryPointSetupPlayer>();

    private void Awake()
    {
        _fabrica = GetComponent<FabricaEnemy>();
        _basicEnemy = Resources.Load<Enemy>("Enemy");
        _fastEnemy = Resources.Load<Enemy>("FastEnemy");
        _player = Resources.Load<PlayerMovement>("Player");
        _timer = Resources.Load<Timer>("Timer");
        _winWindow = Resources.Load<WinWindow>("WinPanel");
        _failWindow = Resources.Load<FailWindow>("FailPanel");
        //_tickBonus = Resources.Load<TimeTickBonus>("BonusTick");
        _bonus = Resources.Load<Bonus>("Sword");
        _bonusHealth = Resources.Load<BonusHealth>("Health");
        //_setupBonusTimer.Add(_tickBonus);
        CreateUI();
        CreatePlayer();
        CreateEnemy();
        CreateBonusKillEnemy();
        CreateBonusHealth();
        Setup();
        _respawnTick = StartCoroutine(RespawnTick());
    }

    private void CreateUI()
    {
        // TimeTickBonus bonus =
        //     Instantiate(_tickBonus, _tickBonus.transform.localPosition, Quaternion.identity, _canvas);
        // bonus.GetComponent<RectTransform>().localPosition = _tickBonus.GetComponent<RectTransform>().localPosition;
        
        _timerCreated = Instantiate(_timer, _timer.transform.localPosition, Quaternion.identity, _canvas);
        _timerCreated.GetComponent<RectTransform>().localPosition = _timer.GetComponent<RectTransform>().localPosition;
        if (_respawnTick != null)
            _timerCreated.OnEnd += () => StopCoroutine(_respawnTick);

        WinWindow winWindow = Instantiate(_winWindow, _winWindow.transform.localPosition, Quaternion.identity, _canvas);
        winWindow.GetComponent<RectTransform>().localPosition = _winWindow.GetComponent<RectTransform>().localPosition;
        _setupTimer.Add(winWindow);

        FailWindow failWindow =
            Instantiate(_failWindow, _failWindow.transform.localPosition, Quaternion.identity, _canvas);
        failWindow.GetComponent<RectTransform>().localPosition =
            _failWindow.GetComponent<RectTransform>().localPosition;
        _setupPlayers.Add(failWindow);
    }

    private void CreateEnemy()
    {
        foreach (Transform point in _enemyStartPoint)
        { 
            _createEnemy = _fabrica.CreateBasicEnemy(_basicEnemy, point);
            _setupPlayers.Add(_createEnemy);
        }
        
        foreach (Transform pointf in _enemyFastStartPoint)
        {
            _createFastEnemy = _fabrica.CreateFastEnemy(_fastEnemy, pointf);
            _setupPlayers.Add(_createFastEnemy);
        }
    }

    private void CreatePlayer()
    {
        _createdPlayer = Instantiate(_player, _playerStartPlayer.position, Quaternion.identity);
    }

    private void CreateBonusKillEnemy()
    {
        foreach (var point in _startPoinBonusKillEnemy)
        {
            _createdBonus = Instantiate(_bonus, point.position, Quaternion.identity);
        }
    }

    private void CreateBonusHealth()
    {
        foreach (var point in _startPointBonusHealth)
        {
            _bonusHealthCreated = Instantiate(_bonusHealth, point.position, Quaternion.identity);
        }
    }
    
    private void Setup()
    {
        foreach (IEntryPointSetupPlayer setupPlayer in _setupPlayers)
        {
            setupPlayer.Setup(_createdPlayer);
        }

        foreach (IEntryPointTimerSetup setupTimer in _setupTimer)
        {
            setupTimer.SetupTimer(_timerCreated);
        }

        // foreach (IEntryPointSetupPlayer bonusTimer in _setupBonusTimer)
        // {
        //     bonusTimer.Setup(_createdPlayer);
        // }
    }

    private IEnumerator RespawnTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            CreateEnemy();
            Setup();
        }
    }
}