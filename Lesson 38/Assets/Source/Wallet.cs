using System;
using UnityEngine;

public class Wallet : MonoBehaviour, ISetupBigHouseble
{
    public Action<float> OnCoinsChange;

    [SerializeField] private BigHouse _house;
    
    public void SetupBigHose(BigHouse house)
    {
        _house = house;
    }
    
    [field: SerializeField] public float CurrentCoins { get; private set; }

    private void Start()
    {
        _house.OnCoinChange += AddCoins;
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
    }

    private void RemoveAmount(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        CurrentCoins -= amount;
        OnCoinsChange?.Invoke(CurrentCoins);
    }
}