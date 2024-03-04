using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    public Action OnScoreChange;
    public Action OnCreateBonusChange;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isDie;
    [SerializeField] private int _randomChance;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(_damage);
        }
    }

    private void Die()
    {
        OnScoreChange?.Invoke();
        OnCreateBonusChange?.Invoke();
        _isDie = true;
        Destroy(gameObject);
    }
}