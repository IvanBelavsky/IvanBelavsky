using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class ClickText : MonoBehaviour
{
    [SerializeField] private Diamond _diamond;
    [SerializeField] private TextMeshProUGUI _text;
    private Coroutine _hideTextTick;

    private void Awake()
    {
        _diamond.OnClickText += TextClick;
        _diamond.OnColorText += ColorText;
        _text.enabled = false;
    }

    private void TextClick()
    {
        transform.position = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(1, 3));
        _text.text = "Click!";
        _text.enabled = true;
        transform.DOScale(4f, 0.5f).OnComplete(() =>
        {
            transform.DOScale(1f, 0.5f);
        });
        transform.DOShakePosition(UnityEngine.Random.Range(-1, 2), 30, 10, 90, false);
        _hideTextTick = StartCoroutine(HideTextTick());
    }

    private void ColorText()
    {
        _text.color = Color.blue;
    }

    void HideText()
    {
        _text.enabled = false;
    }

    private IEnumerator HideTextTick()
    {
        yield return new WaitForSeconds(1f);
        HideText();
    }
}

