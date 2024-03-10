using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamagable, ISetupBigHouseble
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private int _value;

    private BigHouse _house;
    private Rigidbody _rigidbody;
    private bool _isDie;

    public void SetupBigHose(BigHouse house)
    {
        if (house == null)
            throw new ArgumentException("House is null");
        _house = house;
    }

    private void SetupHouse(BigHouse house)
    {
        if (house != null)
            house.AddCoin(_value);
    }

    [field: SerializeField] public float Health { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (_isDie)
            return;
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        Vector3 direction = new Vector3(_house.transform.position.x - transform.position.x, 0,
            _house.transform.position.z - transform.position.z).normalized;
        _rigidbody.velocity = new Vector3(direction.x * _speed, _rigidbody.velocity.y, direction.z * _speed);
    }

    private void Die()
    {
        SetupHouse(_house);
        _isDie = true;
        Destroy(gameObject);
    }
}