using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private Transform _checkGround;

    [SerializeField] private List<AudioClip> _stepsAudio;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private RaycastHit _hit;
    private AudioSource _audioSource;

    private float _velocity;
    private float _velocityRight;
    private float _velocityLeft;
    private bool _isGround;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();
        GroundChecked();
        Jump();
    }

    public void Step()
    {
        _audioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);
        int randomSteps = UnityEngine.Random.Range(0, _stepsAudio.Count);
        _audioSource.PlayOneShot(_stepsAudio[randomSteps]);
    }

    public void Dance()
    {
        _animator.SetBool("IsDance", true);
    }

    public void StpoDance()
    {
        _animator.SetBool("IsDance", false);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _animator.SetBool("IsJump", true);
            _rigidbody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
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

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _velocity += (0.5f * _speed) * Time.deltaTime;
            _animator.SetFloat("VelocityY", _velocity);
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

        _velocity = Mathf.Clamp(_velocity, -1, 1);
        Vector3 moveDirection = ((transform.rotation * Vector3.forward) * _speed) * _velocity;
        _rigidbody.velocity = new Vector3(moveDirection.x, _rigidbody.velocity.y, moveDirection.z);
    }
}