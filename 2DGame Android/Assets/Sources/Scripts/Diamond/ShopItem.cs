using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    public Action OnUpdate;
    [field: SerializeField] public float ValuerPerClick { get; private set; }
    [field: SerializeField] public float ValuerPerSecond { get; private set; }
    [field: SerializeField] public float Price;
    [SerializeField] private Abilities _abilities;
    [SerializeField] private Wallet _wallet;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
    }

    private void TryBuy()
    {
        if (_wallet.TrySpend(Price))
        {
            Price *= 1.25f;
            OnUpdate?.Invoke();
            _abilities.AddValuePerClick(ValuerPerClick);
            _abilities.AddValuePerSecond(ValuerPerSecond);
        }
    }
}
