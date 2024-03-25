using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpawnerEnemies))]
[RequireComponent(typeof(FactoryBonus))]
public class EntryPoint : MonoBehaviour
{
    [Header("Start points position"), Space(5)] [SerializeField]
    private Transform _pointPlayer;

    [SerializeField] private RectTransform _pointScore;
    [SerializeField] private RectTransform _pointHealthUI;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpawnerEnemies _spawnEnemy;
    
    private FactoryBonus _factoryBonus;
    private PlayerHealth _player;
    private PlayerHealth _createdPlayer;
    private Score _score;
    private Score _createdScore;
    private HealthUI _healthUI;
    private HealthUI _createdHealthUI;

    private void Awake()
    {
        _spawnEnemy = GetComponent<SpawnerEnemies>();
        _factoryBonus = GetComponent<FactoryBonus>();
        _player = Resources.Load<PlayerHealth>("Player/Player");
        _score = Resources.Load<Score>("UI/Score");
        _healthUI = Resources.Load<HealthUI>("UI/Health");
    }

    private void OnEnable()
    {
        CreatePlayer();
        CreateScore();
        CreateHealthUI();
        _spawnEnemy.Setup(_createdPlayer);
        _spawnEnemy.Setup(_createdScore);
        _spawnEnemy.Setup(_factoryBonus);
    }

    private void CreateScore()
    {
        _createdScore = Instantiate(_score, _pointScore.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
    }

    private void CreateHealthUI()
    {
        _createdHealthUI = Instantiate(_healthUI, _pointHealthUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
        _createdHealthUI.Setup(_createdPlayer);
    }

    private void CreatePlayer()
    {
        _createdPlayer = Instantiate(_player, _pointPlayer.transform.position, Quaternion.identity);
    }
}