using TMPro;
using UnityEngine;
using System;

public class WalletViewer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _counter;

    private void Awake()
    {
        _wallet.OnValueChange += UpdateView;
    }

    private void UpdateView(float amount)
    {
        _counter.text = Math.Round(amount, 1).ToString();
        if (amount > 1000)
            _counter.text = $"{Math.Round(amount / 1000, 1)}K";
    }
}
