using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Typies
{
    Stone,
    Scissors,
    Paper
}

public class Player : MonoBehaviour
{
    public Action OnClickButton;

    [field: SerializeField] public Typies Type { get; private set; }

    [SerializeField] private Button _stone;
    [SerializeField] private Button _paper;
    [SerializeField] private Button _scissors;
    [SerializeField] private Button _reloud;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _stone.onClick.AddListener(() => ClickButtonStone());
        _paper.onClick.AddListener(() => ClickButtonPaper());
        _scissors.onClick.AddListener(() => ClickButtonScissors());
        _reloud.onClick.AddListener(Restart);
    }

    public void Check()
    {
        if (_enemy.Type == Type)
            Debug.Log("Draw");
        
        else if (_enemy.Type == Typies.Paper && Type == Typies.Scissors ||
                 _enemy.Type == Typies.Stone && Type == Typies.Paper ||
                 _enemy.Type == Typies.Scissors && Type == Typies.Stone)
            Debug.Log("Winner");
        
        else
            Debug.Log("Losing");
    }

    private void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void ClickButtonStone()
    {
        Type = Typies.Stone;
        _paper.gameObject.SetActive(false);
        _scissors.gameObject.SetActive(false);
        OnClickButton?.Invoke();
    }

    private void ClickButtonPaper()
    {
        Type = Typies.Paper;
        _stone.gameObject.SetActive(false);
        _scissors.gameObject.SetActive(false);
        OnClickButton?.Invoke();
    }

    private void ClickButtonScissors()
    {
        Type = Typies.Scissors;
        _paper.gameObject.SetActive(false);
        _stone.gameObject.SetActive(false);
        OnClickButton?.Invoke();
    }
}