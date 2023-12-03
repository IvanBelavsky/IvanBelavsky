using TMPro;
using UnityEngine;
using System;

public class CounterUI : MonoBehaviour
{
    public Action<int> OnCountChange;
    public int Cpounter { get; private set; }

    [SerializeField] private TextMeshProUGUI _text;

    public void AddCount(int counter)
    {
        Cpounter += counter;
        OnCountChange?.Invoke(Cpounter);
        UpdateUI();
    }

    public void RemoveCount(int counter)
    {
        Cpounter -= counter;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _text.text = "Count: " + Cpounter.ToString();
    }
}
