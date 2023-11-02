using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int _count;
    [SerializeField] TextMeshProUGUI _counter;
    public void SetCounter(TextMeshProUGUI counter)
    {
        _counter = counter;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
            _count++;
        _counter.text = "Count:" + _count.ToString();
        Destroy(collision.gameObject);
    }
}
