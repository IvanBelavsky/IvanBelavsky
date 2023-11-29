using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScale : MonoBehaviour, ITargetable
{
    [SerializeField] private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bird>())
        {
            TargetChange();
            collision.gameObject.transform.localScale *= 2;
        }
    }

    public void TargetChange()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}

