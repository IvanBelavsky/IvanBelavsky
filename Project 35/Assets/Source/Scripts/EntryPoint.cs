using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FactoryEnemy))]
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _pointPlayer;
    [SerializeField] private Transform _pointSpawner;

    private Player _player;
    private Player _createdPlayer;
    private Enemy _createdCactus;
    private Enemy _createdStone;
    private FactoryEnemy _factory;
    private Coroutine _spawnTick;


    private void Awake()
    {
        _player = Resources.Load<Player>("Player");
        _factory = GetComponent<FactoryEnemy>();
        CreatePlayer();
    }

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _spawnTick != null)
            StopCoroutine(_spawnTick);
    }

    private void CreatePlayer()
    {
        _createdPlayer = Instantiate(_player, _pointPlayer.transform.position, Quaternion.identity);
    }

    private void CreatedCactus()
    {
        _createdCactus = _factory.CreateCactus(_pointSpawner.transform.position);
        _createdPlayer.SetupEnemy(_createdCactus);
    }

    private void CreateStone()
    {
        _createdStone = _factory.CreateStone(_pointSpawner.transform.position);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            int randomValueTime = UnityEngine.Random.RandomRange(2, 4);
            yield return new WaitForSeconds(randomValueTime);
            CreateStone();
            yield return new WaitForSeconds(randomValueTime);
            CreatedCactus();
        }
    }
}