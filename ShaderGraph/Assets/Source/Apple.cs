using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(SphereCollider))]

public class Apple : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private Material _material;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    private void OnMouseDown()
    {
        Disolve();
    }

    private void Disolve()
    {
        _material.DOFloat(0, "_Volue", 7);
    }
}
