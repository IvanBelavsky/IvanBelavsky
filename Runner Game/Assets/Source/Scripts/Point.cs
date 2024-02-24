using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Factory))]
public class Point : MonoBehaviour
{
    [SerializeField] private Target target;

    private Factory _factory;

    private void Awake()
    {
        _factory = GetComponent<Factory>();
    }

    private void Start()
    {
        _factory.CreateTarget(transform.position);
        _factory.CreateEnemy(transform.position);
    }
}