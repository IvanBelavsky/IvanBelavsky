using UnityEngine;

public class EnemyAmmo : AmmoBasic
{
    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        _rigidbody.velocity = Vector2.down * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(_damage);
            Die();
        }
    }
}