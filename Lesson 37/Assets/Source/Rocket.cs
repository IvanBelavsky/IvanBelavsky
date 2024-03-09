using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public Action<float> OnCoinChange;

    [SerializeField] private ShopItemFuelcan _shopItem;
    
    private Coroutine _fuelAmountTick;

    [field: SerializeField] public float FuelAmount { get; private set; }

    private void Start()
    {
        _shopItem.OnUpdateFuelcan += FuelcanAmountUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _fuelAmountTick = StartCoroutine(FuelAmountTick());
        
        if (Input.GetKeyDown(KeyCode.K))
            PlayerPrefs.DeleteAll();
    }

    private void OnDisable()
    {
        _shopItem.OnUpdateFuelcan -= FuelcanAmountUp;
    }

    public void SetupShopItemFuelcan(ShopItemFuelcan shopItem)
    {
        _shopItem = shopItem;
    }

    public void AddCoins(int coin)
    {
        OnCoinChange?.Invoke(coin);
    }

    public void AddFuelAmount(float amount)
    {
        if(amount<=0)
            return;
        FuelAmount = amount;
    }
    
    private void FuelcanAmountUp()
    {
        FuelAmount *= 2;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private IEnumerator FuelAmountTick()
    {
        while (FuelAmount>0)
        {
            yield return new WaitForSeconds(1);
            FuelAmount--;
        }
        ReloadScene();
    }
}