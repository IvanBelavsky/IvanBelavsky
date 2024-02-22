using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _delay;

    private Factory _factory;
    public Coroutine _spawnTick;


    private void Awake()
    {
        _factory = GetComponent<Factory>();
    }

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    public List<Enemy> SetEnemies()
    {
        return _enemies;
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            float randomPosition = UnityEngine.Random.Range(0, 10);
            _enemies.Add(_factory.CreateEnemy(transform.position));
            yield return new WaitForSeconds(_delay);
            _enemies.Add(_factory.CreateEnemyArmor(transform.position));
            _factory.CreateBox(new Vector3(randomPosition, 0, randomPosition));
            yield return new WaitForSeconds(_delay);
        }
    }
}