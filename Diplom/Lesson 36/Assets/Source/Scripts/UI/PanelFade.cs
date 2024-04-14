using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelFade : MonoBehaviour
{
    [SerializeField] private float _duration;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void FadeOut(Action action)
    {
        _image.DOFade(0, _duration).OnComplete(() => action?.Invoke());
    }
    
    public void FadeIn(Action action)
    {
        _image.DOFade(1, _duration).OnComplete(() => action?.Invoke());
    }
}
