using System;
using System.Collections;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour, ISetupBigHouseble
{
    [SerializeField] private float _delaySpawnTick;

    private BigHouse _house;
    private Enemy _enemy;
    private Coroutine _spawnTick;

    public void SetupBigHose(BigHouse house)
    {
        _house = house;
    }

    private void Awake()
    {
        _enemy = Resources.Load<Enemy>("Enemy");
    }

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    private void CreateEnemy(Vector3 position)
    {
        Instantiate(_enemy, position, Quaternion.identity).SetupBigHose(_house);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            CreateEnemy(transform.position);
            yield return new WaitForSeconds(_delaySpawnTick);
        }
    }
}