using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] TextMeshProUGUI _counter;
    [SerializeField] private float _shootDelay;
    [SerializeField] private bool _canShoot;
    [SerializeField] private float _delay;
    [SerializeField] private float _timeReload;
    [SerializeField] public int _numberBalls;
    [SerializeField] private int _valueBall;
    private Coroutine _reload;
    private void Start()
    {
        _numberBalls = _valueBall;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot == true)
        {
            StartCoroutine(ShootTick());
        }
        if (Input.GetKeyDown(KeyCode.R) && _reload == null)
        {
            _counter.text = "Loading...";
            _reload = StartCoroutine(Reload());
            _reload = null;
        }
    }
    private void CreateBall()
    {
        if (_numberBalls != 0)
        {
            _canShoot = false;
            Ball ballCreated = Instantiate(_ball, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
            ballCreated.Fly(_spawnPoint.transform.forward, 50);
            StartCoroutine(Delay());
            _numberBalls--;
        }
        else
        {
            _counter.text = "You're out of ammo, press R to reload!";
            _canShoot = false;
        }
    }
    private IEnumerator ShootTick()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootDelay);
        CreateBall();
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _canShoot = true;
    }
    private void ReloadGun()
    {
        _numberBalls = _valueBall;
        _canShoot = true;
    }
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_timeReload);
        ReloadGun();
        _counter.text = "Shoot at the targets!!!";
    }
    public void CounterSet(TextMeshProUGUI counter)
    {
        _counter = counter;
    }
}
