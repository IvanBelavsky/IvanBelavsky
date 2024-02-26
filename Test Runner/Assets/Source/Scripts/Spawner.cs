using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    
    private Coroutine _spawnTick;
    private Target _target;

    private void Awake()
    {
        _target = Resources.Load<Target>("Target");
    }

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    private void CreatedEnemy()
    {
        Instantiate(_target, transform.position, Quaternion.identity);
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            
            CreatedEnemy();
            yield return new WaitForSeconds(_delay);
        }
    }
}
