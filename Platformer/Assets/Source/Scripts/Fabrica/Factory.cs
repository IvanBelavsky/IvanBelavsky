using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Factory : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Enemy _enemy;
    private Box _box;

    private void Awake()
    {
        _enemy = Resources.Load<Enemy>("Enemy");
        _box = Resources.Load<Box>("Box");
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        Enemy enemy = Instantiate(_enemy, position, Quaternion.identity).SetArmor(1).SetHealth(5);
        enemy.SetPlayer(_player);
        return enemy;
    }
    public Enemy CreateEnemyArmor(Vector3 position)
    {
        Enemy enemy = Instantiate(_enemy, position, Quaternion.identity).SetArmor(20).SetHealth(10);
        enemy.SetPlayer(_player);
        return enemy;
    }

    public Box CreateBox(Vector3 position)
    {
        return Instantiate(_box, position, Quaternion.identity);
    }
}