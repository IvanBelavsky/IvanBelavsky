using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _lifeTime;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravity;
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            Destroy(gameObject);
        }
    }
    
}