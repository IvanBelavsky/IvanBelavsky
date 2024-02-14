using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private int _delay;

    private Coroutine _spawnTick;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = Resources.Load<Enemy>("Enemy");
    }

    private void Start()
    {
       _spawnTick = StartCoroutine(SpawnTick());
    }

    public List<Enemy> SetEnemies()
    {
        return _enemies;
    }

    private void CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemy, transform.position, Quaternion.identity);
        enemy.SetPlayer(_player);
        _enemies.Add(enemy);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(_delay);
        }
    }
}