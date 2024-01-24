using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _checkGround;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private RaycastHit _hit;
    private Coroutine _fightTick;

    private float _velocity;
    private float _velocityRight;
    private float _velocityLeft;
    private float _force = 1;
    private bool _isGround;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        GroundChecked();
        RotateCamera();
        Attack();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _animator.SetBool("IsJump", true);
            _rigidbody.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }
    }

    private void GroundChecked()
    {
        _isGround = false;
        if (Physics.SphereCast(_checkGround.transform.position, 1, -_checkGround.transform.up, out _hit, 0.5f))
        {
            if (_hit.transform.TryGetComponent(out Ground ground))
            {
                _isGround = true;
                _animator.SetBool("IsJump", false);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.SetBool("IsFight", true);
            _fightTick = StartCoroutine(FightTick());
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _velocity = Mathf.Clamp(_velocity, -1, 0.5f);
            _velocity += (0.5f * _speed) * Time.deltaTime;
            _animator.SetFloat("VelocityY", _velocity);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.Q))
        {
            _velocity = Mathf.Clamp(_velocity, -1, 1);
            _velocity = 1;
            _animator.SetFloat("VelocityY", _velocity);
            _speed *= 2;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _velocity -= (1 * _speed) * Time.deltaTime;
            _animator.SetFloat("VelocityY", _velocity);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _velocityRight = Mathf.Clamp(_velocityRight, -1, 1);
            _velocityRight += (1 * _speed) * Time.deltaTime;
            _animator.SetFloat("VelocityX", _velocityRight);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _velocityLeft = Mathf.Clamp(_velocityLeft, -1, 1);
            _velocityLeft -= (1 * _speed) * Time.deltaTime;
            _animator.SetFloat("VelocityX", _velocityLeft);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            _velocity = 0;
            _animator.SetFloat("VelocityY", _velocity);
            _animator.SetFloat("VelocityX", _velocity);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _velocity = Mathf.Clamp(_velocity, -1, 1);
            _velocity = 1;
            _speed = Mathf.Clamp(_velocity, 1, 4);
            _speed *= 3f;
        }

        _velocity = Mathf.Clamp(_velocity, -1, 1);
        _animator.SetFloat("Velocity", _velocity);
        Vector3 moveDirection = ((transform.rotation * Vector3.forward) * _speed) * _velocity;
        _rigidbody.velocity = new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;

        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }

    private IEnumerator FightTick()
    {
        yield return new WaitForSeconds(1);
        _animator.SetBool("IsFight", false);
    }
}