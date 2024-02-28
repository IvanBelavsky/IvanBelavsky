using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Stone : Enemy, IMoveble
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _health = 1;
        _damage = 1;
       Destroy();
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if(player.IsInvize)
                return;
            player.TakeDamage(_damage);
        }
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector2( -_speedMove, _rigidbody.velocity.y);
    }
}
