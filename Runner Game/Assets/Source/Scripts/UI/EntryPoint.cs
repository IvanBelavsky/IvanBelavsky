using TMPro;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("StartPointsPosition")]
    [SerializeField] private Transform _playerStartPoint;
    [SerializeField] private Transform _spawnerStartPoint;
    [SerializeField] private Transform _scoreBoxStartPoint;
    [SerializeField] private Transform _counterStartPoint;
    [SerializeField] private Transform _parallaxStartPoint;
    
    [Space (20)]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;

    private Player _player;
    private Player _createdPlayer;
    private Spawner _createdSpawner;
    private Spawner _spawner;
    private Score _score;
    private Score _createdScore;
    private TextMeshProUGUI _count;
    private Transform _parallax;
    private Transform _createdParallax;

    private void Awake()
    {
        _player = Resources.Load<Player>("Player");
        _spawner = Resources.Load<Spawner>("Spawner");
        _score = Resources.Load<Score>("ScoreBox");
        _count = Resources.Load<TextMeshProUGUI>("Count");
        _parallax = Resources.Load<Transform>("Parallaxes");
        CreatePlaer();
        CreateParallax();
        CreateSpawner();
        CreateScoreBox();
    }

    private void CreatePlaer()
    {
        _createdPlayer = Instantiate(_player, _playerStartPoint.GetComponent<RectTransform>().localPosition,
            Quaternion.identity, _canvas.transform);
        _camera.GetComponent<CameraShake>().SetUpPlaer(_createdPlayer);
    }

    private void CreateSpawner()
    {
        _createdSpawner = Instantiate(_spawner, _spawnerStartPoint.position, Quaternion.identity, _canvas.transform);
    }

    private void CreateScoreBox()
    {
        _createdScore = Instantiate(_score, _scoreBoxStartPoint.position, Quaternion.identity, _canvas.transform);
        _createdScore.SetupCount(CreatedCounter());
    }

    private TextMeshProUGUI CreatedCounter()
    {
       return _count = Instantiate(_count, _counterStartPoint.position, Quaternion.identity, _canvas.transform);
    }

    private void CreateParallax()
    {
        _createdParallax = Instantiate(_parallax, _parallaxStartPoint.position, Quaternion.identity, _canvas.transform);
    }
}