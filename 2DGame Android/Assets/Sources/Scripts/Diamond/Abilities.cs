using UnityEngine;
using System;

public class Abilities : MonoBehaviour
{
    [field: SerializeField] public float ValuerPerClick { get; private set; } = 1;
    [field: SerializeField] public float ValuerPerSecond { get; private set; } = 0;

    public void AddValuePerClick(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        ValuerPerClick += amount;
    }

    public void AddValuePerSecond(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive");
        ValuerPerSecond += amount;
    }
}
