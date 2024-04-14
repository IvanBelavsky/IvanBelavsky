using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(SpawnerEnemies))]
public class EntryPoint : MonoBehaviour
{
    [Header("Start points position"), Space(5)] [SerializeField]
    private Transform _pointParallax;

    [SerializeField] private Transform _pointHealthUI;
    [SerializeField] private Transform _pointScoreUI;
    [SerializeField] private Canvas _canvas;

    private SpawnerEnemies _spawnEnemy;
    private HealthUI _healthUI;
    private HealthUI _createdHealthUI;
    private ScoreUI _scoreUI;
    private ScoreUI _createdScoreUI;
    private Parallax _parallax;
    private Parallax _createdParallax;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _spawnEnemy = GetComponent<SpawnerEnemies>();
        _healthUI = Resources.Load<HealthUI>(AssetsPath.UI.Health);
        _scoreUI = Resources.Load<ScoreUI>(AssetsPath.UI.Score);
        _parallax = Resources.Load<Parallax>(AssetsPath.UI.Parallax);
    }

    private void OnEnable()
    {
        CreateHealthUI();
        CreateScoreUI();
        CreateParallax();
    }

    private void Start()
    {
        _spawnEnemy.Setup(_createdScoreUI);
    }

    private void CreateScoreUI()
    {
        _createdScoreUI = _diContainer.InstantiatePrefabForComponent<ScoreUI>(_scoreUI,
            _pointScoreUI.GetComponent<RectTransform>().position, Quaternion.identity,
            null);
        _createdScoreUI.transform.SetParent(_canvas.transform, false);
        _createdScoreUI.transform.position = _pointScoreUI.GetComponent<RectTransform>().position;
    }

    private void CreateHealthUI()
    {
        _createdHealthUI = _diContainer.InstantiatePrefabForComponent<HealthUI>(_healthUI,
            _pointHealthUI.GetComponent<RectTransform>().position, Quaternion.identity,
            null);
        _createdHealthUI.transform.SetParent(_canvas.transform, false);
        _createdHealthUI.transform.position = _pointHealthUI.GetComponent<RectTransform>().position;
    }

    private void CreateParallax()
    {
        _createdParallax = _diContainer
            .InstantiatePrefab(_parallax, _pointParallax.transform.position, Quaternion.identity, null)
            .GetComponent<Parallax>();
    }
}