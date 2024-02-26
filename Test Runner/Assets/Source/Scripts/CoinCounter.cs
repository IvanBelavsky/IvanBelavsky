using System;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private Target _target;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    public void SetUp(Target target)
    {
        _target = target;
        if (_target != null)
            _target.OnCoinChange += AddCoin;
    }

    private void AddCoin()
    {
        _count++;
        _text.text = "Coins: " + _count.ToString();
    }
}