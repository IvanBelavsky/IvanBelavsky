using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Enemy _enemy;
    private int _scrore;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (_enemy != null)
            _enemy.OnScoreChange += AddScore;
    }

    private void OnDisable()
    {
        if (_enemy != null)
            _enemy.OnScoreChange -= AddScore;
    }

    public void Setup(Enemy enemy)
    {
        if (_enemy != null)
            _enemy.OnScoreChange += AddScore;

        _enemy = enemy;
    }

    private void AddScore()
    {
        _scrore++;
        _text.text = "Score: " + _scrore.ToString();
    }
}