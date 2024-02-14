using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _health = 1;
    [SerializeField] private Player _player;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move(_player);
    }

    public void SetPlayer(Player player) => _player = player;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
           _player.AddCoins();
            Die();
        }
    }

    private void Die() => Destroy(gameObject);

    private void Move(Player player)
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        _rigidbody.velocity = (direction * _speed);
    }
}