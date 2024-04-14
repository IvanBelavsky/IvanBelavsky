using System;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextTranslate))]
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private EnemyHealth _enemyHealth;
    private GameBehaviourUI _mainMenu;
    private SaveService _saveService;
    private TextTranslate _textTranslate;
    private AnalyticsService _analyticsService;
    private int _score;

    [Inject]
    public void Constructor(GameBehaviourUI mainMenu, SaveService saveService, AnalyticsService analyticsService)
    {
        _mainMenu = mainMenu;
        _saveService = saveService;
        _analyticsService = analyticsService;
    }

    public void AddEnemy(EnemyHealth enemyHealth)
    {
        if (_enemyHealth != null)
            _enemyHealth.OnScoreChange += AddScore;

        _enemyHealth = enemyHealth;
    }

    [field: SerializeField] public string ID = "ScoreVolue";

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textTranslate = GetComponent<TextTranslate>();
    }

    private void Start()
    {
        if (_mainMenu != null)
            _mainMenu.OnClickMainMenuButton += SaveScore;
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
        _mainMenu.OnClickMainMenuButton -= SaveScore;
    }

    private void SaveScore()
    {
        _saveService.CurrentSaveData.AddData(ID, new ScoreSaveData(_score, ID, typeof(ScoreUI)));
        _saveService.Save();
        Debug.Log("Save");
    }

    private void LoadScore()
    {
        if (_saveService.CurrentSaveData.TryGetData<ScoreSaveData>(ID, out ScoreSaveData scoreSaveData))
        {
            _score = scoreSaveData.Score;
            _text.text = _textTranslate.GetTranslateById("score") + ": " + _score.ToString();
        }
        else
        {
            _score = 0;
            _text.text = _textTranslate.GetTranslateById("score") + ": " + _score.ToString();
        }

        Debug.Log("Load");
    }

    private void AddScore()
    {
        _score++;
        _text.text = _textTranslate.GetTranslateById("score") + ": " + _score;
        _analyticsService.TrackEventWithParams("ScoreUpdated", "Score", _score);
    }
}

[Serializable]
public class ScoreSaveData : SaveDatas
{
    public ScoreSaveData(int score, string id, Type type) : base(id, type)
    {
        Score = score;
    }

    public int Score { get; private set; }
}