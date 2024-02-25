using UnityEngine;

public class Factory : MonoBehaviour
{
    private Target _target;
    private Enemy _enemy;

    private void Awake()
    {
        _target = Resources.Load<Target>("Target");
        _enemy = Resources.Load<Enemy>("Enemy");
    }

    public Target CreateTarget(Vector2 position)
    {
         return Instantiate(_target, position, Quaternion.identity);
    }
    
    public Enemy CreateEnemy(Vector2 position)
    {
         return Instantiate(_enemy, position, Quaternion.identity);
    }
}