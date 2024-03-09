using System;
using UnityEngine;

[RequireComponent(typeof(RoketMovement))]
public class RoketInput : MonoBehaviour
{
    public Action OnMove;
    
    private RoketMovement _roketMovement;

    private void Awake()
    {
        _roketMovement = GetComponent<RoketMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _roketMovement.Move();
        }

        _roketMovement.Rotate();

    }
}