using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    private Enemy _enemyRed;
    private Enemy _enemyGreen;
    private Enemy _enemyYellow;
    private ButtonsUI _buttonsUI;


    private void Awake()
    {
        _enemyRed = Resources.Load<Enemy>("Enemies/RedEnemy");
        _enemyGreen = Resources.Load<Enemy>("Enemies/GreenEnemy");
        _enemyYellow = Resources.Load<Enemy>("Enemies/YellowEnemy");
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }

    public Enemy CreateEnemyRed(Vector2 position)
    {
        Enemy redEnemy = Instantiate(_enemyRed, position, Quaternion.Euler(0, 0, 180));
        redEnemy.Setup(_buttonsUI);
        redEnemy.GetComponent<EnemyMovement>().Setup(_buttonsUI);
        redEnemy.GetComponent<EnemyShoot>().Setup(_buttonsUI);
        return redEnemy;
    }

    public Enemy CreateEnemyGreen(Vector2 position)
    {
        Enemy greenEnemy = Instantiate(_enemyGreen, position, Quaternion.Euler(0, 0, 180));
        greenEnemy.Setup(_buttonsUI);
        greenEnemy.GetComponent<EnemyMovement>().Setup(_buttonsUI);
        greenEnemy.GetComponent<EnemyShoot>().Setup(_buttonsUI);
        return greenEnemy;
    }

    public Enemy CreateEnemyYellow(Vector2 position)
    {
        Enemy yellowEnemy = Instantiate(_enemyYellow, position, Quaternion.Euler(0, 0, 180));
        yellowEnemy.Setup(_buttonsUI);
        yellowEnemy.GetComponent<EnemyMovement>().Setup(_buttonsUI);
        yellowEnemy.GetComponent<EnemyShoot>().Setup(_buttonsUI);
        return yellowEnemy;
    }
}