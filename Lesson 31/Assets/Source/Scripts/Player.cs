using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Action OnCounterChange;
    [field: SerializeField] public int Coins { get; private set; }

    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private List<Spawner> _spawners;

    private List<Enemy> _enemies;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DiscoverEnemies();
    }

    public void AddCoins()
    {
        Coins++;
    }

    private void DiscoverEnemies()
    {
        foreach (var spawner in _spawners)
        {
            _enemies = spawner.SetEnemies();
            List<Enemy> buffer = new List<Enemy>(_enemies);
            foreach (Enemy enemy in buffer)
            {
                if (Vector3.Distance(enemy.transform.position, _rigidbody.position) < _attackRange)
                {
                    enemy.TakeDamage(_damage);
                    _enemies.Remove(enemy);
                    OnCounterChange?.Invoke();
                }
            }
        }
    }
}