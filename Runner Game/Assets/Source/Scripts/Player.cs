using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _minValueY, _maxValueY;

    private Rigidbody2D _rigidbody;
    private Vector2 _targetPosition;
    private ParticleSystem _effect;
    private Animator _animator;
    private Coroutine _dieTick;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _effect = Resources.Load<ParticleSystem>("Effect");
    }

    private void Update()
    {
        if (_health <= 0)
            Die();
        Movement();
    }

    private void Movement()
    {
        transform.position =
            new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _minValueY, _maxValueY));

        if (Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.velocity = Vector2.up * _speed;
            Instantiate(_effect, transform.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _rigidbody.velocity = Vector2.down * _speed;
            Instantiate(_effect, transform.position, Quaternion.identity);
        }

        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Target target))
        {
            _health -= target.Damage;
            target.Hit();
        }

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health -= enemy.Damage;
            enemy.Die();
        }
    }

    private void Die()
    {
        _dieTick = StartCoroutine(DieTick());
    }

    private IEnumerator DieTick()
    {
        _animator.SetBool("IsDie", true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}