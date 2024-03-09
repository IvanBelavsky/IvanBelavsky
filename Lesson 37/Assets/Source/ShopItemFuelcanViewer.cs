using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ShopItemFuelcan))]
public class ShopItemFuelcanViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;

    private ShopItemFuelcan _shopItem;

    private void Awake()
    {
        _shopItem = GetComponent<ShopItemFuelcan>();
    }

    private void OnEnable()
    {
        _shopItem.OnUpdateInfo += UpdateInfo;
    }

    private void Start()
    {
        UpdateInfo();
    }

    private void OnDisable()
    {
        _shopItem.OnUpdateInfo -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        _price.text = $"Price: {Math.Round(_shopItem.Price, 1)}";
    }
}
