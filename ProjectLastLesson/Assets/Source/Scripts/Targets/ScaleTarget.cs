using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ScaleTarget : Target
{
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ammo>())
        {
            _count++;
            transform.localScale = new Vector3(7, 7, 7);
        }
        _counter.text = "Count:" + _count.ToString();
        transform.localScale = new Vector3(7, 7, 7);
        Destroy(collision.gameObject);
    }
}
