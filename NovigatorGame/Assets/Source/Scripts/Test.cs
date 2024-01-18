using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f).SetLoops(-1, LoopType.Restart);
    }
}
