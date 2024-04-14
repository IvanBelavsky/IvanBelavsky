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
        _enemyHealthGreen = Resources.Load<EnemyHealth>(AssetsPath.Enemy.EnemyHealthGreen);
        _enemyHealthYellow = Resources.Load<EnemyHealth>(AssetsPath.Enemy.EnemyHealthYellow);
    }

    public EnemyHealth CreateEnemyRed(Vector2 position)
    {
        EnemyHealth redEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthRed, position, Quaternion.Euler(0, 0, 180), null)
            .GetComponent<EnemyHealth>();
        return redEnemyHealth;
    }

    public EnemyHealth CreateEnemyGreen(Vector2 position)
    {
        EnemyHealth greenEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthGreen, position, Quaternion.Euler(0, 0, 180), null)
            .GetComponent<EnemyHealth>();
        return greenEnemyHealth;
    }

    public EnemyHealth CreateEnemyYellow(Vector2 position)
    {
        EnemyHealth yellowEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthYellow, position, Quaternion.Euler(0, 0, 180), null)
            .GetComponent<EnemyHealth>();
        return yellowEnemyHealth;
    }
}