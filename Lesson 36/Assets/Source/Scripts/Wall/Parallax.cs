using System;
using UnityEngine;

public class Parallax : MonoBehaviour, IPauseble
{
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _startSpeed;

    private ButtonsUI _buttons;

    private void Start()
    {
        _startSpeed = _speed;
        _buttons.OnClickPauseButton += PlayPause;
        _buttons.OnClickPlayButton += Continue;
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        _buttons.OnClickPauseButton -= PlayPause;
        _buttons.OnClickPlayButton -= Continue;
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttons = buttonsUI;
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
