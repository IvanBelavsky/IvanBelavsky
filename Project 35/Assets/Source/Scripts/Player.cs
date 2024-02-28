using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamagable, ISetupEnemy
{
    [SerializeField] private float _health;
    [SerializeField] private float _force;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;

    private Enemy _cactus;
    private Rigidbody2D _rigidbody;
    private Coroutine _invizeTick;
    private bool _isPlane;

    [field: SerializeField] public bool IsDie { get; private set; }
    [field: SerializeField] public bool IsInvize { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Attack();
        if (Input.GetKeyDown(KeyCode.K) && _invizeTick != null)
            StopCoroutine(_invizeTick);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Stone spike))
        {
            spike.TakeDamage(_damage);
        }

        if (other.gameObject.TryGetComponent(out Plane plane))
        {
            _isPlane = true;
        }
    }

    public void SetupEnemy(Enemy enemy)
    {
        _cactus = enemy;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (IsDie)
            return;
        _health -= damage;
        InvizeActive();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDie = true;
        Destroy(gameObject);
        ReloudScene();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isPlane)
        {
            _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            _isPlane = false;
        }
    }

    private void InvizeActive()
    {
        _invizeTick = StartCoroutine(InvizeTick());
    }

    private void ReloudScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Attack()
    {
        if (_cactus == null)
            return;
        if (Vector2.Distance(_cactus.transform.position, _rigidbody.position) < _attackRange &&
            Input.GetKeyDown(KeyCode.E))
        {
            _cactus.TakeDamage(_damage);
        }
    }

    private IEnumerator InvizeTick()
    {
        IsInvize = true;
        yield return new WaitForSeconds(3);
        IsInvize = false;
    }
}