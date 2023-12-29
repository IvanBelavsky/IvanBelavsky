using System;
using System.Collections;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<float> OnValueChange;
    [SerializeField] private Diamond _diamond;
    [SerializeField] private float _value;
    [SerializeField] private Abilities _abilities;
    private Coroutine _secondTick;

    private void Awake()
    {
        _diamond.OnClick += Click;
        _secondTick = StartCoroutine(SecondTick());
    }

    private void OnDisable()
    {
        _diamond.OnClick -= Click;
    }

    public bool TrySpend(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        if (_value < amount)
            return false;
        else
        {
            RemoveAmount(amount);
            return true;
        }
    }

    private void Click()
    {
        AddAmount(_abilities.ValuerPerClick);
    }

    private void AddAmount(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        _value += amount;
        OnValueChange?.Invoke(_value);
    }
    private void RemoveAmount(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        _value -= amount;
        OnValueChange?.Invoke(_value);
    }

    private IEnumerator SecondTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            AddAmount(_abilities.ValuerPerSecond);
        }
    }
}
