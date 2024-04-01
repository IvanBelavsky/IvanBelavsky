using System;
using UnityEngine;

public class AmmoPlayer : AmmoBasic
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
        _rigidbody.velocity = Vector2.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Die();
        }
    }
}