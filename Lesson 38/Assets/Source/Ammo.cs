using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    private Rigidbody _rigidbody;

    public Ammo Shoot(Enemy enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        return this;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}