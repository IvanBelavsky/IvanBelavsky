using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Target : MonoBehaviour
{
    [SerializeField] private protected int _count;
    [SerializeField] private protected TextMeshProUGUI _counter;

    public void SetCounter(TextMeshProUGUI counter)
    {
        _counter = counter;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ammo>() || collision.gameObject.GetComponent<Ball>() || collision.gameObject.GetComponent<Bomm>())
            _count++;
        _counter.text = "Count:" + _count.ToString();
        Destroy(collision.gameObject);
    }
}
