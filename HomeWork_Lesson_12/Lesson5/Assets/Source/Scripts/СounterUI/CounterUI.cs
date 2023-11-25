using UnityEngine;
using TMPro;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _count;

    public void AddCount(int amount)
    {
        if (amount < 0)
            return;
        _count += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _text.text = "Count:" + _count.ToString();
    }
}
