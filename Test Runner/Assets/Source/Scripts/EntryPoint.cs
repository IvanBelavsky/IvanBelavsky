using System.Collections;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _startPointPlayer;
    [SerializeField] private Transform _startPointEnemy;
    [SerializeField] private CoinCounter _counter;

    [SerializeField] private Canvas _canvas;

    private Player _player;
    private Player _createdPlayer;
    private Target _enemy;
    private Target _createdenemy;

    private void Awake()
    {
        _player = Resources.Load<Player>("Player");
        _enemy = Resources.Load<Target>("Target");
    }

    private void Start()
    {
        CreatePlayer();
        CreateEnemy();
        CreateCounter();

    }
    
    private void CreatePlayer()
    {
        _createdPlayer = Instantiate(_player, _startPointPlayer.position, Quaternion.identity, _canvas.transform);
    }

    private void CreateEnemy()
    {
        _createdenemy = Instantiate(_enemy, _startPointEnemy.position, Quaternion.identity, _canvas.transform);
        _counter.SetUp(_createdenemy);
    }

    private void CreateCounter()
    {
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            CreateEnemy();
        }
    }
}
