using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _pointRocket;
    [SerializeField] private Transform _pointSpawner;
    [SerializeField] private RectTransform _pointShopItemSpeed;
    [SerializeField] private RectTransform _pointShopItemFuelcan;
    [SerializeField] private RectTransform _pointCoins;
    [SerializeField] private Canvas _canvas;

    private Rocket _rocket;
    private Rocket _createdRocked;
    private SpawnBonus _spawner;
    private SpawnBonus _createdSpawner;
    private ShopItemSpeed _shopItemSpeed;
    private ShopItemSpeed _createdShopItemSpeed;
    private ShopItemFuelcan _shopItemFuelcan;
    private ShopItemFuelcan _createdShopItemFuelcan;
    private TextMeshProUGUI _currentCoins;
    private TextMeshProUGUI _createdCurrentCoins;

    private void Awake()
    {
        _rocket = Resources.Load<Rocket>("Rocket");
        _spawner = Resources.Load<SpawnBonus>("Spawner");
        _shopItemSpeed = Resources.Load<ShopItemSpeed>("ShopItemSpeed");
        _shopItemFuelcan = Resources.Load<ShopItemFuelcan>("ShopItemFuelcan");
        _currentCoins = Resources.Load<TextMeshProUGUI>("Coins");
    }

    private void Start()
    {
        CreateUI();
        CreateRocket();
        CreateSpawner();
    }

    private void CreateRocket()
    {
        _createdRocked = Instantiate(_rocket, _pointRocket.transform.position, Quaternion.Euler(-90, 0, 0));
        _createdRocked.GetComponent<RoketMovement>().SetupShopItemSpeed(_createdShopItemSpeed);
        _createdRocked.GetComponent<WalletViewer>().SetupCounter(_createdCurrentCoins);
        _createdRocked.SetupShopItemFuelcan(_createdShopItemFuelcan);
        _createdShopItemSpeed.SetupWallet(_createdRocked.GetComponent<Wallet>());
        _createdShopItemFuelcan.SetupWallet(_createdRocked.GetComponent<Wallet>());
    }

    private void CreateSpawner()
    {
        _createdSpawner = Instantiate(_spawner, _pointSpawner.transform.position, Quaternion.identity);
        _createdSpawner.SetupRocket(_createdRocked);
    }

    private void CreateUI()
    {
        _createdShopItemSpeed = Instantiate(_shopItemSpeed, _pointShopItemSpeed.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
        _createdShopItemFuelcan = Instantiate(_shopItemFuelcan,
            _pointShopItemFuelcan.GetComponent<RectTransform>().position, Quaternion.identity, _canvas.transform);
        _createdCurrentCoins = Instantiate(_currentCoins, _pointCoins.GetComponent<RectTransform>().position,
            Quaternion.identity,
            _canvas.transform);
    }
}