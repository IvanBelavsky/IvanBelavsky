using UnityEngine;
using Zenject;

public class FactoryEnemy : MonoBehaviour
{
    private EnemyHealth _enemyHealthRed;
    private EnemyHealth _enemyHealthGreen;
    private EnemyHealth _enemyHealthYellow;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer bDiContainer)
    {
        _diContainer = _diContainer;
    }

    private void Awake()
    {
        _enemyHealthRed = Resources.Load<EnemyHealth>("Enemies/RedEnemy");
        _enemyHealthGreen = Resources.Load<EnemyHealth>("Enemies/GreenEnemy");
        _enemyHealthYellow = Resources.Load<EnemyHealth>("Enemies/YellowEnemy");
    }

    public EnemyHealth CreateEnemyRed(Vector2 position)
    {
        EnemyHealth redEnemyHealth =_diContainer
            .InstantiatePrefab(_enemyHealthRed, position, Quaternion.Euler(0, 0, 180),null)
            .GetComponent<EnemyHealth>();
        return redEnemyHealth;
    }

    public EnemyHealth CreateEnemyGreen(Vector2 position)
    {
        EnemyHealth greenEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthGreen, position, Quaternion.Euler(0, 0, 180),null)
            .GetComponent<EnemyHealth>();
        return greenEnemyHealth;
    }

    public EnemyHealth CreateEnemyYellow(Vector2 position)
    {
        EnemyHealth yellowEnemyHealth = _diContainer
            .InstantiatePrefab(_enemyHealthYellow, position, Quaternion.Euler(0, 0, 180),null)
            .GetComponent<EnemyHealth>();
        return yellowEnemyHealth;
    }
}