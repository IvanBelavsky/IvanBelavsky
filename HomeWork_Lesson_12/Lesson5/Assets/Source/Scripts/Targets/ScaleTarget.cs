using UnityEngine;
using System;

public class ScaleTarget : Target
{
    public Action OnDestoy;

    private void Awake()
    {
       OnDestoy += DestoyScaleTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            Hit();
        }
    }

    public override void Hit()
    {
        transform.localScale *= 2;
        OnDestoy?.Invoke();
    }

    private void DestoyScaleTarget()
    {
        if (transform.localScale == new Vector3(12, 12, 12))
        {
            Destroy(gameObject);
        }
    }
}
