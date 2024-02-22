using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private float _speed;

    private Player _player;
    private Rigidbody _rigidbody;

    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Armor { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move(_player);
    }

    public void SetPlayer(Player player) => _player = player;
    
    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage is not negative!");
        float averageDamage = CalculateDamageAfterArmor(damage);
        Health -= averageDamage;
        if (Health <= 0)
        {
            _player.AddCoins();
            Die();
        }
    }

    public Enemy SetArmor(float armor)
    {
        Armor = armor;
        return this;
    }

    public Enemy SetHealth(float health)
    {
        Health = health;
        return this;
    }

    private void Die() => Destroy(gameObject);

    private void Move(Player player)
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        _rigidbody.velocity = (direction * _speed);
    }

    private float CalculateDamageAfterArmor(float damage)
    {
        float damageAverage = damage * (Armor / 100f);
        return damage - damageAverage;
    }

}