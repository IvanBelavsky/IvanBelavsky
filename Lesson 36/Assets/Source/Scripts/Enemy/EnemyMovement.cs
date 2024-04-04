using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _offsetDistance;

    private Rigidbody2D _rigidbody;
    private bool _isPause;

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
        if (_isPause)
        {
            _rigidbody.velocity = Vector2.zero;
            _speed = 0;
            _rigidbody.GetComponent<Animator>().enabled = false;
        }
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
        {
            _speed = _startSpeed;
            Move();
            _rigidbody.GetComponent<Animator>().enabled = true;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _offsetDistance);
            _speed *= -1;
        }
    }
}