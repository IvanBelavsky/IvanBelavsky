using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactoryResources))]
public class SpawnResources : MonoBehaviour
{
    [field: SerializeField] private List<Resours> _resorces;

    [SerializeField] private float _delay;

    private FactoryResources _factory;
    private Coroutine _spawnTick;

    private void Awake()
    {
        _factory = GetComponent<FactoryResources>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            _spawnTick = StartCoroutine(SpawnTick());

        if (Input.GetKeyDown(KeyCode.W) && _spawnTick != null)
            StopCoroutine(_spawnTick);
    }

    public void SetResources(List<Resours> resources)
    {
        foreach (Resours resours in _resorces)
            resources.Add(resours);
    }

    public void RemoveResources()
    {
        foreach (Resours resources in _resorces)
            resources.Destroy();

        _resorces.Clear();
    }

    private IEnumerator SpawnTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);

            Resours resources = _factory.CreateResources(transform.position);

            _resorces.Add(resources);
        }
    }
}