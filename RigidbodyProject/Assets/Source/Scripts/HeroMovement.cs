using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _velocityClimp;
    [SerializeField] float _jumpForce;
    [SerializeField] bool _canClimp = false;
    [SerializeField] bool _canJump = false;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        if (Input.GetKey(KeyCode.E) && _canClimp == true)
        {
            _rigidbody.velocity = Vector3.up * _velocityClimp;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.localScale *= 2;
            _rigidbody.mass = 50;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.localScale /= 2;
            _rigidbody.mass = 5;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rope>())
            _canClimp = true;
        if (collision.gameObject.GetComponent<Ground>())
            _canJump = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rope>())
            _canClimp = false;
        if (collision.gameObject.GetComponent<Ground>())
            _canJump = false;
    }
}
