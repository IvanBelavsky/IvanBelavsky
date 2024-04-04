using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour, IPauseble
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _lifeTime;

    private Rigidbody2D _rigidbody;
    private ButtonsUI _buttonsUI;
    private Vector2 _velocity;
    private bool _isPause;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravity;
        _rigidbody.velocity = _velocity;
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
        _buttonsUI.OnClickPauseButton += PlayPause;
        _buttonsUI.OnClickPlayButton += Continue;
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
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
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