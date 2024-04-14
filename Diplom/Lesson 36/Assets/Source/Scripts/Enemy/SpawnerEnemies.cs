using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(FactoryEnemy))]
public class SpawnerEnemies : MonoBehaviour, IPauseble
{
    [Header("Start points position"), Space(5)]
    [SerializeField] private Transform _pointRedEnemy;
    [SerializeField] private Transform _pointGreenEnemy;
    [SerializeField] private Transform _pointYellowEnemy;

    [Header("Options"), Space(5)] [SerializeField]
    private int _minRows;

    [SerializeField] private int _maxRows;
    [SerializeField] private float _distanceRows;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _takeTimeBonus;
    [SerializeField] private float _delayRandomValue;
    [SerializeField] private int _chanceRed;
    [SerializeField] private int _chanceGreen;
    [SerializeField] private int _chanceYellow;

    private FactoryEnemy _factory;
    private FactoryBonus _factoryBonus;
    private PlayerHealth _player;
    private EnemyHealth _createdRedEnemyHealth;
    private EnemyHealth _createdYellowEnemyHealth;
    private EnemyHealth _createdGreenEnemyHealth;
    private ScoreUI _scoreUI;
    private Coroutine _spawnEnemiesTick;
    private Coroutine _randomValueChanceTick;
    private Coroutine _takeBonusTick;
    private PauseService _pauseService;
    private int _randomChance;
    private bool _isPause;

    [Inject]
    public void Constructor(PauseService pauseService, PlayerHealth player,FactoryBonus factoryBonus)
    {
        _pauseService = pauseService;
        _player = player;
        _factoryBonus = factoryBonus;
    }
    
    public void Setup(ScoreUI scoreUI)
    {
        _scoreUI = scoreUI;
    }
    
    private void Awake()
    {
        _factory = GetComponent<FactoryEnemy>();
        _pauseService.AddPauses(this);
    }

    private void OnEnable()
    {
        _player.OnTakeBonus += TakeBonus;
    }

    private void Start()
    {
        _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
        _randomValueChanceTick = StartCoroutine(RandomValueChanceTick());
    }

    private void OnDisable()
    {
        _player.OnTakeBonus -= TakeBonus;
       _pauseService.RemovePauses(this);
    }

    public void PlayPause()
    {
        _isPause = true;
        if (_spawnEnemiesTick != null && _isPause)
            StopCoroutine(_spawnEnemiesTick);
        if (_randomValueChanceTick != null && _isPause)
            StopCoroutine(_randomValueChanceTick);
        if (_takeBonusTick != null && _isPause)
            StopCoroutine(_takeBonusTick);
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
        {
            _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
            _randomValueChanceTick = StartCoroutine(RandomValueChanceTick());
            _takeBonusTick = StartCoroutine(TakeBonusTick());
        }
    }

    private void CreateRedEnemy()
    {
        _createdRedEnemyHealth =_factory.CreateEnemyRed(_pointRedEnemy.transform.position);
        _scoreUI.AddEnemy(_createdRedEnemyHealth);
        _createdRedEnemyHealth.OnCreateBonusChange += () =>
        {
            if (_createdRedEnemyHealth != null && _randomChance == _chanceRed)
                _factoryBonus.CreateBonus(_createdRedEnemyHealth.transform.position);
        };
    }

    private void CreateGreenEnemy()
    {
        _createdGreenEnemyHealth = _factory.CreateEnemyGreen(_pointGreenEnemy.transform.position);
        _scoreUI.AddEnemy(_createdGreenEnemyHealth);
        _createdGreenEnemyHealth.OnCreateBonusChange += () =>
        {
            if (_createdGreenEnemyHealth != null && _randomChance == _chanceGreen)
                _factoryBonus.CreateBonus(_createdGreenEnemyHealth.transform.position);
        };
    }

    private void CreateYellowEnemy()
    {
        _createdYellowEnemyHealth = _factory.CreateEnemyYellow(_pointYellowEnemy.transform.position);
        _scoreUI.AddEnemy(_createdYellowEnemyHealth);
        _createdYellowEnemyHealth.OnCreateBonusChange += () =>
        {
            if (_createdYellowEnemyHealth != null && _randomChance == _chanceYellow)
                _factoryBonus.CreateBonus(_createdYellowEnemyHealth.transform.position);
        };
    }

    private void TakeBonus()
    {
        if (!_isPause)
            _takeBonusTick = StartCoroutine(TakeBonusTick());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int randomRowCount = UnityEngine.Random.Range(_minRows, _maxRows);
            float randomDelay = UnityEngine.Random.Range(_minDelay, _maxDelay);
            for (int i = 0; i < randomRowCount; i++)
            {
                CreateRedEnemy();
                CreateGreenEnemy();
                CreateYellowEnemy();
                yield return new WaitForSeconds(_distanceRows);
            }

            yield return new WaitForSeconds(randomDelay);
        }
    }

    private IEnumerator TakeBonusTick()
    {
        if (_spawnEnemiesTick != null)
            StopCoroutine(_spawnEnemiesTick);
        yield return new WaitForSeconds(_takeTimeBonus);
        if (!_isPause)
            _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator RandomValueChanceTick()
    {
        while (true)
        {
            _randomChance = UnityEngine.Random.Range(_minValue, _maxValue);
            yield return new WaitForSeconds(_delayRandomValue);
        }
    }
}