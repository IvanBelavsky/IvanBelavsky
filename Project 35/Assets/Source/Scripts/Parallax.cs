using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _startPosition;
    [SerializeField] private float _endPosition;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        if (transform.position.x <= _endPosition)
        {
            Vector2 position = new Vector2(_startPosition, transform.position.y);
            transform.position = position;
        }
    }
}
