using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle :  Weapon
{
    [SerializeField] private float _damage = 2;
    
    public float Damage => _damage;
    
    public override void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }
}
