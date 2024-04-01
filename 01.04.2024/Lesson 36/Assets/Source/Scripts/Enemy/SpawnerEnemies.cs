using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FactoryEnemy))]
public class SpawnerEnemies : MonoBehaviour, IPauseble
{
    [Header("Start points position"), Space(5)] [SerializeField]
    private Transform _pointRedEnemy;

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

    [Header("Bonus settings")] [SerializeField]
    private int _chanceRed;

    [SerializeField] private int _chanceGreen;
    [SerializeField] private int _chanceYellow;

    private FactoryEnemy _factory;
    private FactoryBonus _factoryBonus;
    private PlayerHealth _player;
    private Enemy _createdRedEnemy;
    private Enemy _createdYellowEnemy;
    private Enemy _createdGreenEnemy;
    private ScoreUI _scoreUI;
    private Coroutine _spawnEnemiesTick;
    private Coroutine _randomValueChanceTick;
    private Coroutine _takeBonusTick;
    private ButtonsUI _buttonsUI;
    private int _randomChance;
    private bool _isPause;

    private void Awake()
    {
        _factory = GetComponent<FactoryEnemy>();
        _factory.Setup(_buttonsUI);
    }

    private void OnEnable()
    {
        _player.OnTakeBonus += TakeBonus;
        _buttonsUI.OnClickPauseButton += PlayPause;
        _buttonsUI.OnClickPlayButton += Continue;
    }

    private void Start()
    {
        _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
        _randomValueChanceTick = StartCoroutine(RandomValueChanceTick());
    }

    private void OnDisable()
    {
        _player.OnTakeBonus -= TakeBonus;
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void Setup(ScoreUI scoreUI)
    {
        _scoreUI = scoreUI;
    }

    public void Setup(PlayerHealth player)
    {
        _player = player;
    }

    public void Setup(FactoryBonus bonus)
    {
        _factoryBonus = bonus;
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
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
        _createdRedEnemy = _factory.CreateEnemyRed(_pointRedEnemy.transform.position);
        _scoreUI.Setup(_createdRedEnemy);
        _createdRedEnemy.Setup(_buttonsUI);
        _createdRedEnemy.OnCreateBonusChange += () =>
        {
            if (_createdRedEnemy != null && _randomChance == _chanceRed)
                _factoryBonus.CreateBonus(_createdRedEnemy.transform.position);
        };
    }

    private void CreateGreenEnemy()
    {
        _createdGreenEnemy = _factory.CreateEnemyGreen(_pointGreenEnemy.transform.position);
        _scoreUI.Setup(_createdGreenEnemy);
        _createdGreenEnemy.Setup(_buttonsUI);
        _createdGreenEnemy.OnCreateBonusChange += () =>
        {
            if (_createdGreenEnemy != null && _randomChance == _chanceGreen)
                _factoryBonus.CreateBonus(_createdGreenEnemy.transform.position);
        };
    }

    private void CreateYellowEnemy()
    {
        _createdYellowEnemy = _factory.CreateEnemyYellow(_pointYellowEnemy.transform.position);
        _scoreUI.Setup(_createdYellowEnemy);
        _createdYellowEnemy.Setup(_buttonsUI);
        _createdYellowEnemy.OnCreateBonusChange += () =>
        {
            if (_createdYellowEnemy != null && _randomChance == _chanceYellow)
                _factoryBonus.CreateBonus(_createdYellowEnemy.transform.position);
        };
    }

    private void TakeBonus()
    {
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