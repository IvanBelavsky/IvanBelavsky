using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private EnemyHealth _enemyHealth;
    private int _score;
    private IStorageService _storageService;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _storageService = new JsonToFileStorageService();
    }

    private void Start()
    {
        if (_enemyHealth != null)
            _enemyHealth.OnScoreChange += AddScore;
        LoadScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveScore();
        }
    }

    private void OnDisable()
    {
        if (_enemyHealth != null)
            _enemyHealth.OnScoreChange -= AddScore;
    }

    public void Setup(EnemyHealth enemyHealth)
    {
        if (_enemyHealth != null)
            _enemyHealth.OnScoreChange += AddScore;

        _enemyHealth = enemyHealth;
    }
    
    private void SaveScore()
    {
        ScoreSaveData scoreSaveData = new ScoreSaveData("Score", _score);
        _storageService.Save(scoreSaveData);
        Debug.Log("Save");
    }

    private void LoadScore()
    {
        _storageService.Load<ScoreSaveData>("Score", data =>
        {
            _score = data.Score;
            _text.text = "Score: " + _score.ToString();
            Debug.Log("Load");
        });
    }

    private void AddScore()
    {
        _score++;
        _text.text = "Score: " + _score.ToString();
    }
}

public class ScoreSaveData : SaveData
{
    public ScoreSaveData(string id, int score) : base(id, typeof(ScoreSaveData).FullName)
    {
        Score = score;
    }

    public int Score { get; private set; }
}