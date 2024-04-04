using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _minX, _maxX;
    private bool _isPause;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {
        Move();
    }

    public void PlayPause()
    {
        _isPause = true;
        _speed = 0;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.GetComponent<Animator>().enabled = false;
    }

    public void Continue()
    {
        _isPause = false;
        _rigidbody.GetComponent<Animator>().enabled = true;
        _speed = _startSpeed;
    }

    private void Move()
    {
        if (!_isPause)
        {
            float direction = Input.GetAxis("Horizontal");
            float clampPosition = Mathf.Clamp(_rigidbody.position.x, _minX, _maxX);
            _rigidbody.position = new Vector2(clampPosition, _rigidbody.position.y);
            _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);
        }
    }
}