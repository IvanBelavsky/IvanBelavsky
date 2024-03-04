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
        _enemy.OnScoreChange += AddScore;
    }

    private void OnDisable()
    {
        _enemy.OnScoreChange -= AddScore;
    }

    public void SetupEnemy(Enemy enemy)
    {
        _enemy = enemy;
        if (_enemy != null)
            _enemy.OnScoreChange += AddScore;
    }

    private void AddScore()
    {
        _scrore++;
        _text.text = "Score: " + _scrore.ToString();
    }
}