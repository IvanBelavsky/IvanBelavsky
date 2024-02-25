using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float _decreaseTime;
    [SerializeField] private float _minTime;
    [SerializeField] private float _delay;
    [SerializeField] private List<GameObject> _points;
    
    private Coroutine _spawnTick;

    private void Start()
    {
        _spawnTick = StartCoroutine(SpawnTick());
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            int random = UnityEngine.Random.Range(0, _points.Count);
            Instantiate(_points[random], transform.position, Quaternion.identity);
            if (_delay > _minTime)
            {
                _delay -= _decreaseTime;
            }

            yield return new WaitForSeconds(_delay);
        }
    }
}