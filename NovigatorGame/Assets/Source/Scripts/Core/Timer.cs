using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action OnEnd;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _secondLength;

    private Coroutine _timerTick;

    void Start()
    {
        _timerTick = StartCoroutine(TimerTick());
    }

    private IEnumerator TimerTick()
    {
        while (_secondLength > 0)
        {
            yield return new WaitForSeconds(1);
            _secondLength--;
            _text.text = $"Time: {_secondLength.ToString()}";
        }
        OnEnd?.Invoke();
    }
}