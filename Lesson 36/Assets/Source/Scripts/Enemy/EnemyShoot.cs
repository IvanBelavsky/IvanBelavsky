using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyShoot : MonoBehaviour, IPauseble
{
    [SerializeField] private float _shootTime;
    [SerializeField] private float _shootMin;
    [SerializeField] private float _shootMax;

    private EnemyAmmo _ammo;
    private Coroutine _shootTick;
    private ButtonsUI _buttonsUI;
    private DiContainer _diContainer;
    private bool _isPause;

    [Inject]
    public void Constructor(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _ammo = Resources.Load<EnemyAmmo>("Ammo/EnemyAmmo");
    }

    private void OnEnable()
    {
        _buttonsUI.OnClickPauseButton += PlayPause;
        _buttonsUI.OnClickPlayButton += Continue;
    }

    private void Start()
    {
        _shootTime = Random.Range(_shootMin, _shootMax);
        _shootTick = StartCoroutine(ShootTick());
    }

    private void OnDisable()
    {
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void PlayPause()
    {
        _isPause = true;
        if (_shootTick != null && _isPause)
            StopCoroutine(_shootTick);
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
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
                _diContainer.InstantiatePrefab(_ammo, transform.position, Quaternion.Euler(0, 0, 180), null)
                    .GetComponent<AmmoBasic>();
                _shootTime = Random.Range(_shootMin, _shootMax);
            }
        }
    }
}