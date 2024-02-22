using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public Action OnCounterChange;

    [SerializeField] private float _attackRange;
    [SerializeField] private List<Spawner> _spawners;

    [field: SerializeField] private List<Weapon> _weapons;
    private Weapon _weapon;
    private List<Enemy> _enemies;
    private Rigidbody _rigidbody;
    private int currentWeaponIndex = 0;

    [field: SerializeField] public int Coins { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        int randomType = Random.Range(0, _weapons.Count);
        _weapon = _weapons[randomType];
    }

    private void Update()
    {
        DiscoverEnemies();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            _weapon = _weapons[currentWeaponIndex];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
            _weapon = _weapons[currentWeaponIndex];
        }
    }

    public void AddCoins()
    {
        Coins++;
    }

    private void DiscoverEnemies()
    {
        foreach (Spawner spawner in _spawners)
        {
            _enemies = spawner.SetEnemies();
            List<Enemy> buffer = new List<Enemy>(_enemies);
            foreach (Enemy enemy in buffer)
            {
                if (Vector3.Distance(enemy.transform.position, _rigidbody.position) < _attackRange)
                {
                    while (enemy.Health > 0)
                    {
                        _weapon.Attack(enemy);
                        OnCounterChange?.Invoke();
                        _enemies.Remove(enemy);
                    }
                }
            }
        }
    }
}