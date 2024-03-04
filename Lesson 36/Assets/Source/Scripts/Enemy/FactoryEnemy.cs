using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    private Enemy _enemyRed;
    private Enemy _enemyGreen;
    private Enemy _enemyYellow;

    private void Awake()
    {
        _enemyRed = Resources.Load<Enemy>("RedEnemy");
        _enemyGreen = Resources.Load<Enemy>("GreenEnemy");
        _enemyYellow = Resources.Load<Enemy>("YellowEnemy");
    }

    public Enemy CreateEnemyRed(Vector2 position)
    {
        return Instantiate(_enemyRed, position, Quaternion.Euler(0, 0, 180));
    }

    public Enemy CreateEnemyGreen(Vector2 position)
    {
        return Instantiate(_enemyGreen, position, Quaternion.Euler(0, 0, 180));
    }

    public Enemy CreateEnemyYellow(Vector2 position)
    {
        return Instantiate(_enemyYellow, position, Quaternion.Euler(0, 0, 180));
    }
}