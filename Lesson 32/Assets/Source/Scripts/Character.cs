using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageble
{
    [field: SerializeField] public int Health { get; protected set; } = 100;
    [field: SerializeField] public int Damage { get; protected set; }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy();
    }

    public abstract void Destroy();

    public void Improve()
    {
        Health += 5;
        Damage += 2;
    }
}