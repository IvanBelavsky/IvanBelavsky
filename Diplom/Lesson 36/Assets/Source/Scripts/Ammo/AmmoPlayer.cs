using UnityEngine;

public class AmmoPlayer : AmmoBasic
{
    private void Start()
    {
        SetID();
        SetType(AmmoType.playerType);
    }

    private void Update()
    {
        Move();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(_damage);
            Die();
        }
    }
    
    public override void Move()
    {
        _rigidbody.velocity = Vector2.up * _speed;
    }
}

