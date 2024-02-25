using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _counter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Target target))
        {
            _score++;
            _counter.text = "Count: " + _score.ToString();
        }
    }

    public void SetupCount(TextMeshProUGUI text)
    {
        _counter = text;
    }
}