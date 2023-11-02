using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    [SerializeField] Transform _targets;
    [SerializeField] int _numberTargets;
    [SerializeField] TextMeshProUGUI _counter;
    void Awake()
    {
        for (int i = 0; i < _numberTargets; i++)
        {
            TargetsSpawn();
        }
    }
    private void TargetsSpawn()
    {
        Vector3 positionTargets = new Vector3 ((Random.Range(2f, 10f)), Random.Range(1f, 5f), Random.Range(3f, 18f));
        Instantiate(_targets, positionTargets, Quaternion.identity).GetComponent<Target>().SetCounter(_counter);
    }
}
