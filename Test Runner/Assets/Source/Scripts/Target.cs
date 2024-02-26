using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Action OnCoinChange;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeDie;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Die();
        }
    }

    private void Die()
    {
        OnCoinChange?.Invoke();
        Destroy(gameObject);
    }
}