using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnDie;
    [SerializeField] private float _maxHealth;

    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        _health -= damage;
        if (_health <= 0)
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }     
    }
}
