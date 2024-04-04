using UnityEngine;

public class AmmoPlayer : AmmoBasic
{
    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        _rigidbody.velocity = Vector2.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(_damage);
            Die();
        }
    }
}