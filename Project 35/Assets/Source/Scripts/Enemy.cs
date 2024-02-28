using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _speedMove;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _lifeTime;
    [SerializeField] protected bool _isDie;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (_isDie)
            return;
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    protected void Destroy()
    {
        Destroy(gameObject,_lifeTime);
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}