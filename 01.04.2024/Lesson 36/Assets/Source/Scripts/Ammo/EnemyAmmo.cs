using UnityEngine;

public class EnemyAmmo : AmmoBasic
{
    private void Start()
    {
        _buttonsUI.OnClickPauseButton += PlayPause;
        _buttonsUI.OnClickPlayButton += Continue;
    }
    
    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
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