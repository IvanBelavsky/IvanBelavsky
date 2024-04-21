using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(FactoryAmmo))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float _shootTime;
    [SerializeField] private float _shootMin;
    [SerializeField] private float _shootMax;

    private EnemyAmmo _ammo;
    private FactoryAmmo _factoryAmmo;
    private Coroutine _shootTick;
    private GameBehaviourUI _gameBehaviourUI;
    private DiContainer _diContainer;
    private bool _isPause;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _factoryAmmo = GetComponent<FactoryAmmo>();
        _ammo = Resources.Load<EnemyAmmo>(AssetsPath.Ammo.EnemyAmmo);
    }


    private void Start()
    {
        _shootTime = Random.Range(_shootMin, _shootMax);
        _shootTick = StartCoroutine(ShootTick());
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
                var ammoInstance = _factoryAmmo.CreatedEnemyAmmo(transform.position);
                if (ammoInstance != null)
                {
                    ammoInstance.GetComponent<AmmoBasic>();
                }

                _shootTime = Random.Range(_shootMin, _shootMax);
            }
        }
    }
}