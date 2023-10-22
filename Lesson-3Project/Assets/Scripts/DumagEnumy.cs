using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumagEnumy : MonoBehaviour
{
    [SerializeField] Health _enumyDamag;
    void Start()
    {
        _enumyDamag.TakeDamag(40);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
