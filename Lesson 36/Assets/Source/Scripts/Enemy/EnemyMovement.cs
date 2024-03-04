using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _offsetDistance;


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
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _offsetDistance);
            _speed *= -1;
        }
    }
}