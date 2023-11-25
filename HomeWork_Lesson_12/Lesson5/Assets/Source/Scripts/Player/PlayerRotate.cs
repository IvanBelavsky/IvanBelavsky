using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _rotate;
    [SerializeField] private float _claimX, _claimY;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _rotate += new Vector3(-vertical, horizontal, 0) * _speed;
        _rotate.x = Mathf.Clamp(_rotate.x, -_claimX, _claimX);
        _rotate.y = Mathf.Clamp(_rotate.y, -_claimY, _claimY);
        transform.rotation = Quaternion.Euler(_rotate);
    }
}
