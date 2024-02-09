using System;
using UnityEngine;

public class FactoryResources : MonoBehaviour
{
    private Resours _resources;

    private void Awake() => _resources = Resources.Load<Resours>("Resources");

    public Resours CreateResources(Vector3 position)
    {
        Resours resours = Instantiate(_resources, position, Quaternion.identity).GetComponent<Resours>();
        return resours;
    }
}