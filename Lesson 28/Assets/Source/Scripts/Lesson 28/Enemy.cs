using UnityEngine;

public enum EnemyType
{
    Mage,
    Ogre,
    Armored,
    Fast
}

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemyType Type { get; private set; }
    [field: SerializeField] public float Health { get; private set; }

    public Enemy(EnemyType type, float health)
    {
        Type = type;
        Health = health;
    }

    public Enemy RandomType()
    {
        var type = System.Enum.GetValues(typeof(EnemyType));
        int randomEnum = Random.Range(0, type.Length);
        Type = (EnemyType)randomEnum;
        return this;
    }

    public Enemy RandomHealth()
    {
        Health = Random.Range(0, 100);
        return this;
    }
    
    
}
