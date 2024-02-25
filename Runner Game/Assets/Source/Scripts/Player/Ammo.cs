using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private Coroutine _dieTick;
    
    [field: SerializeField] public float Damage { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Target target))
        {
            Die();
        }

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Die();
            enemy.TakeDamage(Damage);
        }
    }

    private void Die()
    {
        _animator.SetBool("IsDie", true);
        _dieTick = StartCoroutine(DieTick());
    }

    private IEnumerator DieTick()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
