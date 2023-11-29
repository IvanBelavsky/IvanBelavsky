using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBird : MonoBehaviour
{
    [SerializeField] private float _maxAmmo;
    [SerializeField] private float _delay;
    [SerializeField] private ShotRubber _point;
    [SerializeField] private Transform _rubberPosition;
    [SerializeField] private BirdsFactory _factory;
    [SerializeField] private Transform _startPosition;
    private Coroutine _delayTick;
    private float _currenAmmo;

    private void Awake()
    {
        _currenAmmo = _maxAmmo;
        NextBird();
        _point.OnRealseShoot += NextBird;
    }

    private void NextBird()
    {
        if (_currenAmmo <= 0)
            return;
        _currenAmmo--;
        _delayTick = StartCoroutine(DelayTick());
    }

    private void CreatedBird()
    {
        Bird newBird = _factory.BirdCreated(_startPosition.position);
        newBird.SetPoint(_rubberPosition, _startPosition);
        _point.UpdateBird(newBird);
    }

    private IEnumerator DelayTick()
    {
        yield return new WaitForSeconds(_delay);
        CreatedBird();
    }
}
