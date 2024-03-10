using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform[] _pointsTowers;
    [SerializeField] private Transform[] _pointsSpawners;
    [SerializeField] private Transform _pointHouse;
    [SerializeField] private RectTransform _pointCounter;
    [SerializeField] private Canvas _canvas;

    private BigHouse _bigHouse;
    private BigHouse _createdBigHouse;
    private PointTower _pointTower;
    private PointTower _createdPointTowers;
    private TextMeshProUGUI _counter;
    private TextMeshProUGUI _createdCounter;
    private SpawnerEnemy _spawner;
    private SpawnerEnemy _createdSpawner;
    private Wallet _wallet;
    private Wallet _createdWallet;

    private void Awake()
    {
        _bigHouse = Resources.Load<BigHouse>("BigHouse");
        _pointTower = Resources.Load<PointTower>("Point");
        _counter = Resources.Load<TextMeshProUGUI>("CounterCoins");
        _spawner = Resources.Load<SpawnerEnemy>("Spawner");
        _wallet = Resources.Load<Wallet>("Wallet");
    }

    private void Start()
    {
        CreateBigHouse();
        CreateUI();
        CreateWallet();
        CreateSpawner();
        CreatePointTower();
    }

    private void CreateBigHouse()
    {
        _createdBigHouse = Instantiate(_bigHouse, _pointHouse.transform.position, Quaternion.Euler(0, 90, 0));
    }

    private void CreatePointTower()
    {
        foreach (Transform point in _pointsTowers)
        {
            _createdPointTowers = Instantiate(_pointTower, point.transform.position, Quaternion.identity);
            _createdPointTowers.SetupWallet(_createdWallet);
        }
    }

    private void CreateSpawner()
    {
        foreach (Transform point in _pointsSpawners)
        {
            _createdSpawner = Instantiate(_spawner, point.transform.position, Quaternion.identity);
            _createdSpawner.SetupBigHose(_createdBigHouse);
        }
    }

    private void CreateWallet()
    {
        _createdWallet = Instantiate(_wallet, transform.position, Quaternion.identity);
        _createdWallet.SetupBigHose(_createdBigHouse);
        _createdWallet.GetComponent<WalletViewer>().SetupCounter(_createdCounter);
    }

    private void CreateUI()
    {
        _createdCounter = Instantiate(_counter, _pointCounter.GetComponent<RectTransform>().position,
            Quaternion.identity,
            _canvas.transform);
    }
}