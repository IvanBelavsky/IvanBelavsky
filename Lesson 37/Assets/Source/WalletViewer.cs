using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class WalletViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _wallet.OnCoinsChange += UpdateView;
    }

    private void Start()
    {
        _counter.text = "Coins: " + Math.Round(_wallet.CurrentCoins, 1).ToString();
    }

    public void SetupCounter(TextMeshProUGUI counter)
    {
        _counter = counter;
    }

    private void UpdateView(float amount)
    {
        _counter.text = "Coins: " + Math.Round(amount, 1).ToString();
        if (amount > 1000)
            _counter.text = $"{Math.Round(amount / 1000, 1)}K";
    }
}