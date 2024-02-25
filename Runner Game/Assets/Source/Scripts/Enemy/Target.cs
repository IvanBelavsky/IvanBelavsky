using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Target : MonoBehaviour
{
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public float Healh { get; private set; } = 1;

    [SerializeField] private float _speed;
    [SerializeField] private float _timeDie;

    private Animator _animator;
    private Coroutine _dieTick;
    private ParticleSystem _effect;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _effect = Resources.Load<ParticleSystem>("Effect 1");
    }

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    public void Hit()
    {
        _animator.SetBool("IsHit", true);
        _animator.Play("Hit-Animation");
        Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Ammo ammo))
        {
            Healh -= ammo.Damage;
            if (Healh <= 0)
               Hit();
        }
    }

    private void Die()
    {
        _dieTick = StartCoroutine(DieTick());
    }
    
    private IEnumerator DieTick()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_timeDie);
        Destroy(gameObject);
    }
}