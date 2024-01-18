using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Action OnDie;
    public Action<float> OnSetBonus;

    public bool IsTookBonus { get; private set; }

    [SerializeField] private float _maxHealth;

    [SerializeField] private float _health;
    private Coroutine _bonusTick;

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Bonus bonus))
        {
            if (IsTookBonus)
                return;
            IsTookBonus = true;
            _bonusTick = StartCoroutine(BonusTick(bonus.Time));
            OnSetBonus?.Invoke(bonus.Time);
            bonus.Destroy();
        }

        if (other.transform.TryGetComponent(out BonusHealth health))
        {
            _health = 10;
            _bonusTick = StartCoroutine(BonusTick(health.Time));
            OnSetBonus?.Invoke(health.Time);
            Destroy(health.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
        {
            if (IsTookBonus)
                return;
            else
                TakeDamage(1); 
        }
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

    private IEnumerator BonusTick(float time)
    {
        yield return new WaitForSeconds(time);
        IsTookBonus = false;
        _health = _maxHealth;
    }
}