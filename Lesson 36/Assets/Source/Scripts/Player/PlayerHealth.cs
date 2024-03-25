using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour, IDamageble
{
    public Action OnHealthChange;
    public Action OnTakeBonus;

    [SerializeField] private bool _isDie;
    [SerializeField] private float _damage;

    private Destroyer _dieAnimation;

    [field: SerializeField] public float Health { get; private set; }

    private void Awake()
    {
        _dieAnimation = Resources.Load<Destroyer>("Animations/Destroy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }

        if (other.gameObject.TryGetComponent(out Bonus bonus))
        {
            OnTakeBonus?.Invoke();
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (_isDie)
            return;
        Health -= damage;
        OnHealthChange?.Invoke();
        if (Health <= 0)
        {
            Die();
        }
    }

    private void DieAnimation()
    {
        Instantiate(_dieAnimation, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        _isDie = true;
        Destroy(gameObject);
        DieAnimation();
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}