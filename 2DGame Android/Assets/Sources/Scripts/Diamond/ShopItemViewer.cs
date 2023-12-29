using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ShopItemViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private ShopItem _item;

    private void Awake()
    {
        UpdateInfo();
        _item.OnUpdate += UpdateInfo;
    }

    private void UpdateInfo()
    {
        _price.text = $"Price: {Math.Round(_item.Price, 1)}";
        _description.text = "";
        if (_item.ValuerPerClick > 0)
        {
            _description.text += $"Add per click: {_item.ValuerPerClick}";
        }
        if (_item.ValuerPerSecond > 0)
        {
            _description.text += $"Add per second: {_item.ValuerPerSecond}";
        }
    }

}
