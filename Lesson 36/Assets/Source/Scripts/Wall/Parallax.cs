using System;
using UnityEngine;
using Zenject;

public class Parallax : MonoBehaviour, IPauseble
{
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _startSpeed;

    private PauseService _pauseService;

    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
    }

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
    }

    public void PlayPause()
    {
        _speed = 0;
    }

    public void Continue()
    {
        _speed = _startPosition;
    }

    private void Move()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if (transform.position.y <= _endPosition)
        {
            Vector2 position = new Vector2(transform.position.x, _startPosition);
            transform.position = position;
        }
    }
}