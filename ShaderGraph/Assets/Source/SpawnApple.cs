using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AppleFactory))]
public class SpawnApple : MonoBehaviour
{
    [SerializeField] private int _volueApple;
    [SerializeField] private float _delay;
    private AppleFactory _factory;
    private Coroutine _delayTick;

    private void Awake()
    {
        _factory = GetComponent<AppleFactory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _delayTick = StartCoroutine(DelayTick());
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_delayTick != null)
            {
                StopCoroutine(_delayTick);
            }
        }
    }

    private IEnumerator DelayTick()
    {
        Apple apple = null;
        for (int i = 0; i < _volueApple; i++)
        {
            yield return new WaitForSeconds(_delay);
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-8, 9), 2.36f, Random.Range(-9, 8));
            apple = _factory.CreatedApple(randomPosition);

        }
    }
}
