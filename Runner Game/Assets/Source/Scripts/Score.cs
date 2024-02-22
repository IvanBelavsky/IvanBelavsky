using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _counter;

    private void Update()
    {
        _counter.text = _score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Target gear))
        {
            _score++;
            _counter.text = _score.ToString();
        }
    }
}
