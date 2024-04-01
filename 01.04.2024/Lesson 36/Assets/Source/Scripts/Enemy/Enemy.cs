using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyShoot))]
public class Enemy : MonoBehaviour, IDamageble
{
    public event Action OnScoreChange;
    public event Action OnCreateBonusChange;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isDie;
    
    private ButtonsUI _buttonsUI;

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }
    
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