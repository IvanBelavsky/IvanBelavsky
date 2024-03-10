using UnityEngine;

public class PointTower : MonoBehaviour
{
    private Wallet _wallet;
    private Tower _tower;

    public void SetupWallet(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void Awake()
    {
        _tower = Resources.Load<Tower>("Tower");
    }

    private void OnMouseDown()
    {
        CreateTower();
    }

    private void CreateTower()
    {
        Instantiate(_tower, transform.position, Quaternion.identity).SetupWallet(_wallet);
        Destroy(gameObject);
    }
}