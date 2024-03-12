using System.Collections;
using UnityEngine;

public class SpawnerCube : MonoBehaviour
{
    [SerializeField] private float _delay;
    
    private Cube _cube;
    private Coroutine _spawnTick;
    private bool _isSpawn;

    private void Awake()
    {
        _cube = Resources.Load<Cube>("Cube");
    }

    private void Start()
    {
        SpawnCube(transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isSpawn)
            _spawnTick = StartCoroutine(SpawnTick());
    }

    private void SpawnCube(Vector3 position)
    {
        Instantiate(_cube, position, Quaternion.identity);
        _isSpawn = true;
    }

    private IEnumerator SpawnTick()
    {
        _isSpawn = false;
        yield return new WaitForSeconds(_delay);
        SpawnCube(transform.position);
    }
}