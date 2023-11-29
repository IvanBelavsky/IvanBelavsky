using UnityEngine;
using System;

public class Target : MonoBehaviour, ITargetable
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bird>())
        {
            Hit();
            Destroy(collision.gameObject);
        }
    }

    public void Hit()
    {
        transform.localScale *= 2;
    }
}
