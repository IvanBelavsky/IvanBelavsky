using UnityEngine;

public class FactoryCoins : MonoBehaviour
{
    private Coin _coin;

    private void Awake()
    {
        _coin = Resources.Load<Coin>("Coin");
    }

    public void CreateCoins(Vector3 position)
    {
        Instantiate(_coin, position, Quaternion.Euler(0, 90, 90));
    }
}