using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cactus :Enemy, IMoveble
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        _rigidbody.velocity = new Vector2( -_speedMove, _rigidbody.velocity.y);
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
}
