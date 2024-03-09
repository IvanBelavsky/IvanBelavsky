using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ShopItemSpeed))]
public class ShopItemViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _price;

    private ShopItemSpeed _shopItemSpeed;

    private void Awake()
    {
        _shopItemSpeed = GetComponent<ShopItemSpeed>();
    }

    private void OnEnable()
    {
        _shopItemSpeed.OnUpdate += UpdateInfo;
    }

    private void Start()
    {
        UpdateInfo();
    }

    private void OnDisable()
    {
        _shopItemSpeed.OnUpdate -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        _price.text = $"Price: {Math.Round(_shopItemSpeed.Price, 1)}";
    }
}