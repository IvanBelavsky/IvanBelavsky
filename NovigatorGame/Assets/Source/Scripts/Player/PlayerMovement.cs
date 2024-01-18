using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private Transform _checkGround;
    [SerializeField] private bool _isGround;
    private Rigidbody _player;
    private RaycastHit _hit;
    private Animator _animator;


    private void Awake()
    {
        _player = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        CameraRotate();
        GroundChecked();
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            Jump();
    }

    private void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;

        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }

    private void GroundChecked()
    {
        _isGround = false;
        if (Physics.SphereCast(_checkGround.transform.position, 1, -_checkGround.transform.up, out _hit, 0.5f))
        {
            if (_hit.transform.TryGetComponent(out Ground ground))
                _isGround = true;
        }
    }

    private void Jump()
    {
        _player.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
    }

    private void Movement()
    {
        Vector3 inputMove = Vector3.zero;

        inputMove.x = Input.GetAxis("Horizontal");
        inputMove.z = Input.GetAxis("Vertical");

        inputMove *= _speed;

        Vector3 directionMove = transform.right * inputMove.x + transform.forward * inputMove.z;

        _player.velocity = new Vector3(directionMove.x, _player.velocity.y, directionMove.z);
        
        if (_player.velocity.magnitude > 0)
            _animator.Play("Run");
        else
            _animator.Play("Idle");
    }
}