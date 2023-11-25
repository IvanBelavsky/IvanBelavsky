using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private protected CounterUI _countUI;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetCounter(CounterUI counter)
    {
        _countUI = counter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            Hit();
            Destroy(collision.gameObject);
        }
    }

    public void ColorTarget()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public virtual void Hit()
    {
        _countUI.AddCount(1);
    }
}
