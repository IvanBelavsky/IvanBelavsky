using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstCharacter : Character
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Health = Random.Range(10, 20);
        Damage = 5;
    }

    public override void Destroy()
    {
        Destroy(gameObject);
    }
}