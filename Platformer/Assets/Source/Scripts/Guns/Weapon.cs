using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float Damage { get; protected set; }

    public virtual void Attack(Enemy enemy)
    {
        
    }
}