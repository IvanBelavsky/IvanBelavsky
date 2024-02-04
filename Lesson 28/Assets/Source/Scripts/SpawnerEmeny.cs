using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactoryEnemies))]
public class SpawnerEmeny : MonoBehaviour
{
    [field: SerializeField] private List<Enemy> _enemies;
    
    [field: SerializeField] private List<Enemy> _armoredEmenies;
    [field: SerializeField] private List<Enemy> _mageEmenies;
    [field: SerializeField] private List<Enemy> _ogreEmenies;
    [field: SerializeField] private List<Enemy> _fastEmenies;
    
    [SerializeField] private float _delay;

    private FactoryEnemies _factory;
    private Coroutine _spawnTick;

    private void Awake()
    {
        _factory = GetComponent<FactoryEnemies>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _spawnTick = StartCoroutine(SpawnTick());
        }

        if (Input.GetKeyDown(KeyCode.W) && _spawnTick != null)
        {
            StopCoroutine(_spawnTick);
        }
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-20, 20));
            
            yield return new WaitForSeconds(_delay);
            
            Enemy enemyCreated = null;
            enemyCreated = _factory.CreatedEnemies(position);
            
            _enemies.Add(enemyCreated);
            
            AddEnemiesType();
        }
    }
    
    private List<Enemy> GetEnemiesOfType(EnemyType type)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (Enemy enemy in _enemies)
        {
            if (enemy.Type == type)
            {
                enemies.Add(enemy);
            }
        }
        return enemies;
    }

    private void AddEnemiesType()
    {
        _armoredEmenies = GetEnemiesOfType(EnemyType.Armored);
        _mageEmenies = GetEnemiesOfType(EnemyType.Mage);
        _fastEmenies = GetEnemiesOfType(EnemyType.Fast);
        _ogreEmenies = GetEnemiesOfType(EnemyType.Ogre);
    }
}