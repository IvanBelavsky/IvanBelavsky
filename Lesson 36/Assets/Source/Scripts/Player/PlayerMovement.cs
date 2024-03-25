using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minX, _maxX;
    private float direction;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        direction = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);
    }
}