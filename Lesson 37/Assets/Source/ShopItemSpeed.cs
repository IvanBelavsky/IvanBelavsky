using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItemSpeed : MonoBehaviour
{
    public Action OnUpdate;
    public Action OnUpdateSpeed;

    private Button _button;

    [SerializeField] private Wallet _wallet;

    [field: SerializeField] public float Price { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(TryBuy);
        Load();
    }

    public void SetupWallet(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void TryBuy()
    {
        if (_wallet.TrySpend(Price))
        {
            Price *= 1.25f;
            OnUpdate?.Invoke();
            OnUpdateSpeed?.Invoke();
            Save();
        }
    }
    
    private void Load()
    {
        if (PlayerPrefs.HasKey("Price"))
           Price = PlayerPrefs.GetFloat("Price", Price);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Price",Price);
    }
}