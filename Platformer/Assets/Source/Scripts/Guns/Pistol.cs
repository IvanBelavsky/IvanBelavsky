using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private float _damge = 1;

    public float Damage => _damge;

    public override void Attack(Enemy enemy)
    {
        enemy.TakeDamage(_damge);
    }
}