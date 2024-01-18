using System;
using UnityEngine;
using UnityEngine.AI;

public class FabricaEnemy : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = Resources.Load<Enemy>("Enemy");
    }

    public Enemy CreateBasicEnemy(Enemy enemy,Transform point)
    {
        return Instantiate(enemy, point.position, Quaternion.identity).Speed(1.5f);
    }
    
    public Enemy CreateFastEnemy(Enemy enemy,Transform point)
    {
        return Instantiate(enemy, point.position, Quaternion.identity).Speed(7f);
    }
}
