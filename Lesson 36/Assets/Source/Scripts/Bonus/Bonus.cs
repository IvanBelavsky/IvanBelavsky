using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour, IPauseble
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _lifeTime;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private PauseService _pauseService;
    private bool _isPause;

    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravity;
        _rigidbody.velocity = _velocity;
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
    }

    public void PlayPause()
    {
        _isPause = true;
        if (_isPause)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.GetComponent<Animator>().enabled = false;
        }
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
        {
            _rigidbody.gravityScale = 1;
            _rigidbody.velocity = _velocity;
            _rigidbody.GetComponent<Animator>().enabled = true;
        }
    }
}