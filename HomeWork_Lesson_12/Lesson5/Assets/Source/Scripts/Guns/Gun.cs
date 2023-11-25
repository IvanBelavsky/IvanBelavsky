using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    public Action<float, float> OnAmmoChange;

    [SerializeField] private protected Ball _ball;
    [SerializeField] private protected Transform _spawnPoint;
    [SerializeField] private protected TextMeshProUGUI _counter;
    [SerializeField] private protected float _shootDelay;
    [SerializeField] private protected float _delay;
    [SerializeField] private protected float _timeReload;
    [SerializeField] private protected float _maxBalls;
    [field: SerializeField] public float Ball { get; private protected set; }
    [field: SerializeField] public bool CanShoot { get; private protected set; }

    private void Start()
    {
        Ball = _maxBalls;
    }

    public virtual void Shoot()
    {
        if (Ball != 0 && CanShoot == true)
        {
            CanShoot = false;
            Ball ballCreated = Instantiate(_ball, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
            ballCreated.Fly(_spawnPoint.transform.forward, 50);
            Delay();
            AmmoCounter(Ball);
        }
        else
        {
            _counter.text = "You're out of ammo, press R to reload!";
            CanShoot = false;
        }
    }

    public void Relaod()
    {
        StartCoroutine(ReloadTick());
        CanShoot = false;
    }

    private void ReloadGun()
    {
        Ball = _maxBalls;
    }

    private protected void Delay()
    {
        StartCoroutine(DelayTick());
    }

    public void AmmoCounter(float damage)
    {
        if (Ball <= 0)
            return;
        Ball--;
        OnAmmoChange?.Invoke(Ball, _maxBalls);
    }

    private IEnumerator DelayTick()
    {
        yield return new WaitForSeconds(_delay);
        CanShoot = true;
    }

    private IEnumerator ReloadTick()
    {
        _counter.text = "Loading...";
        yield return new WaitForSeconds(_timeReload);
        ReloadGun();
        _counter.text = "Shoot at the targets!!!";
        CanShoot = true;
    }
}
