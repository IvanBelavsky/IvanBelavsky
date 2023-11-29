using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotRubber : MonoBehaviour
{
    public Action OnRealseShoot;

    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;
    private Bird _bird;
    private Vector2 _start;
    private Camera _camera;
    private bool _canShoot;

    private void Awake()
    {
        _camera = Camera.main;
        _start = transform.position;
    }

    public void UpdateBird(Bird bird)
    {
        _bird = bird;
        _canShoot = true;
    }

    private void OnMouseDrag()
    {
        if (!_canShoot)
            return;
        Vector2 target = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(_start, target) < _maxDistance)
        {
            transform.position = target;
        }
        else
        {
            Vector2 direction = (target - _start).normalized * _maxDistance;
            transform.position = _start + direction;
        }
    }
    private void OnMouseUp()
    {
        if (!_canShoot)
            return;
        Vector2 releasePosition = transform.position;
        transform.position = _start;
        Vector2 delte = _start - releasePosition;
        _bird.Launch(delte * _speed);
        _bird = null;
        _canShoot = false;
        OnRealseShoot?.Invoke();
    }
}
