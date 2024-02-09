using UnityEngine;

public class FactoryEnemies : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = Resources.Load<Enemy>("Enemy");
    }

    public Enemy CreatedEnemies(Vector3 position)
    {
        Enemy enemy = Instantiate(_enemy, position, Quaternion.Euler(new Vector3(0, 180, 0))).RandomType().RandomHealth();
        return enemy;
    }
}