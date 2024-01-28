using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;

        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}