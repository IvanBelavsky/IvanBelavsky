using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    [SerializeField] TargetFactory _factory;
    [SerializeField] int _numberTargets;

    void Awake()
    {
        for (int i = 0; i < _numberTargets; i++)
        {
            _factory.TargetsSpawn(transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(2f, 18f)));
            _factory.SpawnMultiCilorTarget(transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(2f, 18f)));
            _factory.SpawnSphere(transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(2f, 18f)));
        }

    }
}
