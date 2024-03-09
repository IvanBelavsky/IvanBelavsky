using System.Collections;
using UnityEngine;


[RequireComponent(typeof(FactoryCoins))]
[RequireComponent(typeof(FactoryFuelCan))]
public class SpawnBonus : MonoBehaviour, ISetupRocket
{
    [SerializeField] private float _minSpawnPositionX;
    [SerializeField] private float _maxSpawnPositionX;
    [SerializeField] private float _maxSpawnPositionY;
    [SerializeField] private float _minSpawnPositionY;
    [SerializeField] private float _timeSpawnCoins;
    [SerializeField] private float _timeSpawnFuelcans;

    private FactoryCoins _factoryCoins;
    private FactoryFuelCan _factoryFuel;
    private Coroutine _spawCoinsTick;
    private Rocket _rocket;

    private void Awake()
    {
        _factoryCoins = GetComponent<FactoryCoins>();
        _factoryFuel = GetComponent<FactoryFuelCan>();
    }

    private void Start()
    {
        _spawCoinsTick = StartCoroutine(SpawnCoinsTick());
        StartCoroutine(SpawnFuelsTick());
    }


    public void SetupRocket(Rocket rocket)
    {
        _rocket = rocket;
    }

    private void SpawnCoins()
    {
        float randomPositionX = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        float randomPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
        Vector3 spawnPosition = new Vector3(_rocket.transform.position.x + randomPositionX,
            _rocket.transform.position.y + randomPositionY, _rocket.transform.position.z);
        _factoryCoins.CreateCoins(spawnPosition);
    }

    private void SpawnFuels()
    {
        float randomPositionX = Random.Range(_minSpawnPositionX, _maxSpawnPositionX);
        float randomPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
        Vector3 spawnPosition = new Vector3(_rocket.transform.position.x + randomPositionX,
            _rocket.transform.position.y + randomPositionY, _rocket.transform.position.z);
        _factoryFuel.CreateeFuel(spawnPosition);
    }

    private IEnumerator SpawnCoinsTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawnCoins);
            SpawnCoins();
        }
    }

    private IEnumerator SpawnFuelsTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeSpawnFuelcans);
            SpawnFuels();
        }
    }
}