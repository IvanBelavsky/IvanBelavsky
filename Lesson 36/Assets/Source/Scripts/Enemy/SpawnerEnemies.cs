using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FactoryEnemy))]
public class SpawnerEnemies : MonoBehaviour
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
    private FactoryBonus _spawnBonus;
    private PlayerHealth _player;
    private Enemy _createdRedEnemy;
    private Enemy _createdYellowEnemy;
    private Enemy _createdGreenEnemy;
    private Score _score;
    private Coroutine _spawnEnemiesTick;
    private Coroutine _randomValueChanceTick;
    private Coroutine _takeBonusTick;
    private int _randomChance;

    private void Awake()
    {
        _factory = GetComponent<FactoryEnemy>();
    }

    private void Start()
    {
        _spawnEnemiesTick = StartCoroutine(SpawnEnemies());
        _randomValueChanceTick = StartCoroutine(RandomValueChanceTick());
        _player.OnTakeBonus += TakeBonus;
    }

    public void Setup(Score score)
    {
        _score = score;
    }

    public void Setup(PlayerHealth player)
    {
        _player = player;
    }

    public void Setup(FactoryBonus bonus)
    {
        _spawnBonus = bonus;
    }

    private void CreateRedEnemy()
    {
        _createdRedEnemy = _factory.CreateEnemyRed(_pointRedEnemy.transform.position);
        _score.Setup(_createdRedEnemy);
        _createdRedEnemy.OnCreateBonusChange += () =>
        {
            if (_createdRedEnemy != null && _randomChance == _chanceRed)
                _spawnBonus.GetComponent<FactoryBonus>().CreateBonus(_createdRedEnemy.transform.position);
        };
    }

    private void CreateGreenEnemy()
    {
        _createdGreenEnemy = _factory.CreateEnemyGreen(_pointGreenEnemy.transform.position);
        _score.Setup(_createdGreenEnemy);
        _createdGreenEnemy.OnCreateBonusChange += () =>
        {
            if (_createdGreenEnemy != null && _randomChance == _chanceGreen)
                _spawnBonus.GetComponent<FactoryBonus>().CreateBonus(_createdGreenEnemy.transform.position);
        };
    }

    private void CreateYellowEnemy()
    {
        _createdYellowEnemy = _factory.CreateEnemyYellow(_pointYellowEnemy.transform.position);
        _score.Setup(_createdYellowEnemy);
        _createdYellowEnemy.OnCreateBonusChange += () =>
        {
            if (_createdYellowEnemy != null && _randomChance == _chanceYellow)
                _spawnBonus.GetComponent<FactoryBonus>().CreateBonus(_createdYellowEnemy.transform.position);
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