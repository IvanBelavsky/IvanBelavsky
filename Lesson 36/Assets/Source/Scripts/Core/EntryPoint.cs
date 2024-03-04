using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FactoryEnemy))]
[RequireComponent(typeof(BonusFactory))]
public class EntryPoint : MonoBehaviour
{
    [Header("Start points position"), Space(5)] [SerializeField]
    private Transform _pointPlayer;

    [SerializeField] private Transform _pointRedEnemy;
    [SerializeField] private Transform _pointGreenEnemy;
    [SerializeField] private Transform _pointYellowEnemy;
    [SerializeField] private RectTransform _pointScore;
    [SerializeField] private RectTransform _pointHealthUI;
    [SerializeField] private Canvas _canvas;

    [Header("Options"), Space(5)] [SerializeField]
    private int _minRows;

    [SerializeField] private int _maxRows;
    [SerializeField] private float _distanceRows;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    [Header("Bonus settings")] [SerializeField]
    private int _minValue;

    [SerializeField] private int _maxValue;
    [SerializeField] private int _chanceRed;
    [SerializeField] private int _chanceGreen;
    [SerializeField] private int _chanceYellow;
    [SerializeField] private float _delayRandomValue;
    [SerializeField] private float _takeTimeBonus;

    private FactoryEnemy _factory;
    private BonusFactory _bonusFactory;
    private PlayerHealth _player;
    private PlayerHealth _createdPlayer;
    private Enemy _createdRedEnemy;
    private Enemy _createdYellowEnemy;
    private Enemy _createdGreenEnemy;
    private Score _score;
    private Score _createdScore;
    private HealthUI _healthUI;
    private HealthUI _createdHealthUI;
    private Coroutine _spawnEnemiesTick;
    private Coroutine _randomValueChanceTick;
    private Coroutine _takeBonusTick;
    private int _randomChance;

    private void Awake()
    {
        _factory = GetComponent<FactoryEnemy>();
        _bonusFactory = GetComponent<BonusFactory>();
        _player = Resources.Load<PlayerHealth>("Player");
        _score = Resources.Load<Score>("Score");
        _healthUI = Resources.Load<HealthUI>("Health");
    }

    private void Start()
    {
        CreatePlayer();
        CreateScore();
        CreateHealthUI();
        _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
        _randomValueChanceTick = StartCoroutine(RandomValueChanceTick());
        _createdPlayer.OnTakeBonus += TakeBonus;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _spawnEnemiesTick != null)
            StopCoroutine(_spawnEnemiesTick);
    }

    private void CreateScore()
    {
        _createdScore = Instantiate(_score, _pointScore.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
    }

    private void CreateHealthUI()
    {
        _createdHealthUI = Instantiate(_healthUI, _pointHealthUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
        _createdHealthUI.SetupPlayer(_createdPlayer);
    }

    private void CreatePlayer()
    {
        _createdPlayer = Instantiate(_player, _pointPlayer.transform.position, Quaternion.identity);
    }

    private void CreateRedEnemy()
    {
        _createdRedEnemy = _factory.CreateEnemyRed(_pointRedEnemy.transform.position);
        _createdScore.SetupEnemy(_createdRedEnemy);
        _createdRedEnemy.OnCreateBonusChange += () =>
        {
            if (_createdRedEnemy != null && _randomChance == _chanceRed)
                _bonusFactory.CreateBonus(_createdRedEnemy.transform.position);
        };
    }

    private void CreateGreenEnemy()
    {
        _createdGreenEnemy = _factory.CreateEnemyGreen(_pointGreenEnemy.transform.position);
        _createdScore.SetupEnemy(_createdGreenEnemy);
        _createdGreenEnemy.OnCreateBonusChange += () =>
        {
            if (_createdGreenEnemy != null && _randomChance == _chanceGreen)
                _bonusFactory.CreateBonus(_createdGreenEnemy.transform.position);
        };
    }

    private void CreateYellowEnemy()
    {
        _createdYellowEnemy = _factory.CreateEnemyYellow(_pointYellowEnemy.transform.position);
        _createdScore.SetupEnemy(_createdYellowEnemy);
        _createdYellowEnemy.OnCreateBonusChange += () =>
        {
            if (_createdYellowEnemy != null && _randomChance == _chanceYellow)
                _bonusFactory.CreateBonus(_createdYellowEnemy.transform.position);
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

    private IEnumerator RandomValueChanceTick()
    {
        while (true)
        {
            _randomChance = UnityEngine.Random.Range(_minValue, _maxValue);
            yield return new WaitForSeconds(_delayRandomValue);
        }
    }

    private IEnumerator TakeBonusTick()
    {
        if (_spawnEnemiesTick != null)
            StopCoroutine(_spawnEnemiesTick);
        yield return new WaitForSeconds(_takeTimeBonus);
        _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
    }
}