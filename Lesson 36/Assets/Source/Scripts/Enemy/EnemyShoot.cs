using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float _shootTime;
    [SerializeField] private float _shootMin;
    [SerializeField] private float _shootMax;

    private EnemyAmmo _ammo;
    private Coroutine _shootTick;

    private void Awake()
    {
        _ammo = Resources.Load<EnemyAmmo>("Ammo/EnemyAmmo");
    }

    private void Start()
    {
        _shootTime = Random.Range(_shootMin, _shootMax);
        _shootTick = StartCoroutine(ShootTick());
    }

    private IEnumerator ShootTick()
    {
        while (true)
        {
            _shootTime--;
            yield return new WaitForSeconds(_shootTime);
            if (_shootTime <= 0)
            {
                Instantiate(_ammo, transform.position, Quaternion.Euler(0, 0, 180));
                _shootTime = Random.Range(_shootMin, _shootMax);
            }
        }
    }
}