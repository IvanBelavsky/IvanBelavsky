using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTarget : Target
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            _count++;
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Random.ColorHSV();
        }
        _counter.text = "Count:" + _count.ToString();
        Destroy(collision.gameObject);
    }
}
