using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Rigidbody _enemyRigidbody;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _speed;
    [SerializeField] private Mass _mass;
    private Vector3 _moveDirection;
    private int _killsEnemyCount;
    void Start()
    {
        _rigidbody.transform.localScale = new Vector3(_mass.Amount, _mass.Amount, _mass.Amount);
    }
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //_moveDirection = new Vector3(horizontal, 0, vertical) * _speed;
        //_rigidbody.velocity = _moveDirection;

        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.velocity = new Vector3(0, 0, _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.velocity = new Vector3(0, 0, -_speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.velocity = new Vector3(-_speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.velocity = new Vector3(_speed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Mass>())
        {
            if (_mass.Amount > collision.gameObject.GetComponent<Mass>().Amount)
            {
                collision.transform.position = (transform.position - collision.transform.position).normalized;
                _enemyRigidbody = GetComponent<Rigidbody>();
                _enemyRigidbody.AddForce(transform.position * _speed, ForceMode.Impulse);
                //_mass.Amount += collision.gameObject.GetComponent<Mass>().Amount;
                //transform.localScale = new Vector3(_mass.Amount, _mass.Amount, _mass.Amount);
                //Destroy(collision.gameObject);
                _killsEnemyCount++;
                Debug.Log("Player killed enemies: " + _killsEnemyCount);
                Debug.Log("Player size: " + _mass.Amount);
            }
            else
            {
                collision.gameObject.GetComponent<Mass>().Amount += _mass.Amount;
                collision.gameObject.transform.localScale = new Vector3(_mass.Amount, _mass.Amount, _mass.Amount);
                Destroy(gameObject);
                Debug.Log("Game over");
            }
        }

    }
}
