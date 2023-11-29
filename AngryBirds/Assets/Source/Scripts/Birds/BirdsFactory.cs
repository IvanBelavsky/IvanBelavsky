using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsFactory : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private CounterUI _counterUI;

    public Bird BirdCreated(Vector2 position)
    {
        Bird birdCreated = Instantiate(_bird, position, Quaternion.identity);
        birdCreated.SetCounter(_counterUI);
        return birdCreated;
    }
}
