using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Wallet : MonoBehaviour
{
    public Action<float> OnCoinsChange;

    [SerializeField] private Rocket _rocket;
    [field: SerializeField] public float CurrentCoins { get; private set; }

    private void Awake()
    {
        _rocket.OnCoinChange += AddCoins;
    }

    private void Start()
    {
        Load();
    }

    public bool TrySpend(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        if (CurrentCoins < amount)
            return false;
        else
        {
            RemoveAmount(amount);
            return true;
        }
    }

    private void AddCoins(float amount)
    {
        if (amount < 0)
            throw new AggregateException("Amount not positive");
        CurrentCoins += amount;
        OnCoinsChange?.Invoke(CurrentCoins);
        Save();
    }

    private void RemoveAmount(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        CurrentCoins -= amount;
        OnCoinsChange?.Invoke(CurrentCoins);
        Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Coins"))
            CurrentCoins = PlayerPrefs.GetFloat("Coins", CurrentCoins);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Coins", CurrentCoins);
    }
}