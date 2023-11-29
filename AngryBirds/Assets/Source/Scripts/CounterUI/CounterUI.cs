using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SlingshotBird _slingshotBird;
    private float _count;

    private void Awake()
    {
        _count = _slingshotBird.MaxAmmo;
    }
    public void RemoveCount(float amount)
    {
        if (amount < 0)
            return;
        _count -= amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _text.text = "Birds:" + _count.ToString();
    }
}
