using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class AngryCube : MonoBehaviour
{
    [SerializeField] private protected float _speed;
    private Rigidbody _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public AngryCube Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speed;
        return this;
    }

    public AngryCube Jump (Vector3 up, Vector3 direction)
    {
        _rigidbody.AddForce(up * _speed, ForceMode.Impulse);
        _rigidbody.velocity *= _speed;
        return this;
    }
}
