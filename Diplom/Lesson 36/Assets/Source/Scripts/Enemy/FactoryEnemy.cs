using UnityEngine;
using Zenject;

public class FactoryEnemy : MonoBehaviour
{
    private EnemyHealth _enemyHealthRed;
    private EnemyHealth _enemyHealthGreen;
    private EnemyHealth _enemyHealthYellow;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _enemyHealthRed = Resources.Load<EnemyHealth>(AssetsPath.Enemy.EnemyHealthRed);
    }

    public EnemyHealth CreateEnemyRed(Vector2 position)
    {
        EnemyHealth redEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthRed, position, Quaternion.Euler(0, 0, 180), null)
            .GetComponent<EnemyHealth>();
        return redEnemyHealth;
    }
}