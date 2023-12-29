using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    private Rigidbody2D _rididbody;
    private Vector2 _touchPosition;

    private void Awake()
    {
        _rididbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _touchPosition) > 0.5f)
        {
            _rididbody.velocity = (_touchPosition - (Vector2)transform.position).normalized * _speed;
        }
        else
        {
            _rididbody.velocity = Vector2.zero;
        }
    }

    public void MoveToPosition(Vector2 position)
    {
        _touchPosition = position;
    }
}
