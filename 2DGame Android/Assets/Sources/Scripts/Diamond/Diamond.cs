using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Diamond : MonoBehaviour
{
    public Action OnClick;
    public Action OnClickText;
    public Action OnEffects;
    public Action OnColorText;

    [SerializeField] private int _counter;
    [SerializeField] private SpriteRenderer _diamond;

    private void OnMouseDown()
    {
        _counter++;
        OnClick?.Invoke();
        OnClickText?.Invoke();
        transform.DOScale(1.5f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(1, 0.2f);
        });
        OnEffects?.Invoke();
        if (_counter >= 100)
            NewSprite();

    }

    private void NewSprite()
    {
        Sprite newSprite = Resources.Load<Sprite>("Dark_Blue");
       _diamond.sprite = newSprite;
        OnColorText?.Invoke();
    }
}
