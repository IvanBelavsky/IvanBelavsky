using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private bool _isShoot;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        
        if(Input.GetKeyDown(KeyCode.Space) && !_isShoot)
            Shoot();
    }

    private void Move()
    {
        if (!_isShoot)
        {
            float direction = Input.GetAxis("Horizontal");
            _rigidbody.velocity = new Vector3(direction * _speed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        }

    }
    
    private void Shoot()
    {
        _rigidbody.AddForce(Vector3.forward * _force, ForceMode.Impulse);
        _isShoot = true;
    }
}
