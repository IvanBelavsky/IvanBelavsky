using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.GraphicsBuffer;

public class TargetFactory : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private CounterUI _counter;
    [SerializeField] private SphereTarget _sphere;

    public void TargetsSpawn(Vector3 spawnPosition)
    {
        Target target = Instantiate(_target, spawnPosition, Quaternion.identity).GetComponent<Target>();
        target.SetCounter(_counter);
    }

    public void SpawnMultiCilorTarget(Vector3 spawnPosition)
    {
        Target target = Instantiate(_target, spawnPosition, Quaternion.identity).GetComponent<Target>();
        target.SetCounter(_counter);
        target.ColorTarget();
    }

    public void SpawnSphere(Vector3 spawnPosition)
    {
        SphereTarget sphere = Instantiate(_sphere, spawnPosition, Quaternion.identity).GetComponent<SphereTarget>();
    }

}
