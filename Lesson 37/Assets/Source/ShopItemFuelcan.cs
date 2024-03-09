using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItemFuelcan : MonoBehaviour
{
    public Action OnUpdateFuelcan;
    public Action OnUpdateInfo;

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
            OnUpdateInfo?.Invoke();
            OnUpdateFuelcan?.Invoke();
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
