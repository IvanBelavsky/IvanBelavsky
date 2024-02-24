using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.OnShakeCameraChange += Shake;
        _player.OnShakeCameraDisable += DisableShake;
    }

    private void OnDisable()
    {
        _player.OnShakeCameraChange -= Shake;
        _player.OnShakeCameraDisable -= DisableShake;
    }

    private void DisableShake()
    {
        _animator.SetBool("IsShake", false);
    }
    
    private void Shake()
    {
        _animator.SetBool("IsShake", true);
    }
}