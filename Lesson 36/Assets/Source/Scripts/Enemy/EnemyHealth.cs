using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyShoot))]
public class EnemyHealth : MonoBehaviour, IDamageble, IPauseble
{
    public event Action OnScoreChange;
    public event Action OnCreateBonusChange;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isDie;

   [SerializeField] private PauseService _pauseService;
    private bool _isPause;

    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
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
    
    public void PlayPause()
    {
        GetComponent<EnemyMovement>().PlayPause();
        GetComponent<EnemyShoot>().PlayPause();
    }

    public void Continue()
    {
        GetComponent<EnemyMovement>().Continue();
        GetComponent<EnemyShoot>().Continue();
    }

    private void Die()
    {
        OnScoreChange?.Invoke();
        OnCreateBonusChange?.Invoke();
        _isDie = true;
        Destroy(gameObject);
    }
}