using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Worker : MonoBehaviour
{
    [field: SerializeField] private List<SpawnResources> _spawnPoint;
    [field: SerializeField] private List<Resours> _resources;
    [SerializeField] private Storage _storage;

    private Rigidbody _rigidbody;
    
    private void Awake()=> _rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(MoveToPoint());
    }

    private void RemoveResources() => _resources.Clear();

    private IEnumerator MoveToPoint()
    {
        while (true)
        {
            foreach (SpawnResources point in _spawnPoint)
            {
                _rigidbody.DOMove(point.transform.position, 1);
                
                while (Vector3.Distance(transform.position, point.transform.position) > 2f)
                    yield return new WaitForSeconds(0.1f);

                point.SetResources(_resources);
                point.RemoveResources();
            }

            _rigidbody.DOMove(_storage.transform.position, 1);
            
            while (Vector3.Distance(transform.position, _storage.transform.position) > 2f)
                yield return new WaitForSeconds(0.1f);

            _storage.SetResources(_resources);
            RemoveResources();
            
            yield return new WaitForSeconds(5f);
        }
    }
}