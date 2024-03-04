using UnityEngine;

public class Prallax : MonoBehaviour
{
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
        if (transform.position.y <= _endPosition)
        {
            Vector2 position = new Vector2(transform.position.x, _startPosition);
            transform.position = position;
        }
    }
}