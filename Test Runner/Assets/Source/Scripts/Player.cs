using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minValueY, _maxValueY, _minValueX, _maxValueX;
    [SerializeField] private int _health;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _dieTick;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_health<=0)
            Die();
        
        transform.position =
            new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _minValueY, _maxValueY)); 
        transform.position =
            new Vector2(Mathf.Clamp(transform.position.x, _minValueX, _maxValueX), transform.position.y);
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.velocity = Vector2.up * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.velocity = Vector2.down * _speed;
        }
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Target target))
        {
           Debug.Log("Lol");
        }
    }

    private void Die()
    {
        _dieTick = StartCoroutine(DieTick());
    }

    private IEnumerator DieTick()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}