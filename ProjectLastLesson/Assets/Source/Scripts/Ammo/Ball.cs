using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbode;

    private void Awake()
    {
        _rigidbode = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 directoin, float force)
    {
        _rigidbode.AddForce(force * directoin, ForceMode.Impulse);
    }
}
