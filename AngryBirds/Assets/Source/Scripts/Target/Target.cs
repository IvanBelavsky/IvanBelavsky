using UnityEngine;
using System;

public class Target : MonoBehaviour, ITargetable
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bird>())
        {
            TargetChange();
            Destroy(collision.gameObject);
        }
    }

    public void TargetChange()
    {
        transform.localScale *= 2;
    }
}
