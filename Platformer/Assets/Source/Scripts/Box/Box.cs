using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health;
    
    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage is not negative!");
        _health -= damage;
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        TakeDamage(5);
    }
}
