using System;
using UnityEngine;

public class Prallax : MonoBehaviour, IPauseble
{
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _startSpeed;

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.B))
            PlayPause();
        if (Input.GetKeyDown(KeyCode.V))
            Continue();
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
