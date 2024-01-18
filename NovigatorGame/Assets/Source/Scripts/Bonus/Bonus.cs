using System;
using DG.Tweening;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [field: SerializeField] public float Time { get; private set; }

    private void Start()
    {
        BonusMove();
    }

    public void Destroy() => Destroy(gameObject);

    public void BonusMove()
    {
        transform.DOMove(transform.position + new Vector3(0, 1, 0), 1).OnComplete(() =>
        {
            transform.DOMove(transform.position + new Vector3(0, -1, 0), 1);
        }).SetLoops(-1, LoopType.Yoyo);

        transform.DORotate(transform.position + new Vector3(0, 180, 0), 2f).OnComplete(() =>
        {
            transform.DORotate(transform.position + new Vector3(0, 10, 0), 2f);
        }).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }
}