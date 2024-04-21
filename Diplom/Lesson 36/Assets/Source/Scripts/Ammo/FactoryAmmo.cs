using UnityEngine;
using Zenject;

public class FactoryAmmo : MonoBehaviour
{
    private AmmoBasic _enemyAmmo;
    private AmmoBasic _playerAmmo;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _enemyAmmo = Resources.Load<EnemyAmmo>(AssetsPath.Ammo.EnemyAmmo);
        _playerAmmo = Resources.Load<AmmoPlayer>(AssetsPath.Ammo.PlayerAmmo);
    }

    public EnemyAmmo CreatedEnemyAmmo(Vector2 position)
    {
        var ammo = _diContainer.InstantiatePrefab(_enemyAmmo, position, Quaternion.Euler(0, 0, 180), null)
            .GetComponent<EnemyAmmo>();
        return ammo;
    }
    
    public AmmoPlayer CreatedPlayerAmmo(Vector2 position)
    {
        var ammo = _diContainer.InstantiatePrefab(_playerAmmo, position, Quaternion.identity, null)
            .GetComponent<AmmoPlayer>();
        return ammo;
    }
}