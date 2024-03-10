using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Tower : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _pointAmmo;
    [SerializeField] private float _multipliedPrice;
    [SerializeField] private float _multipliedRadius;

    private Ammo _ammo;
    private SphereCollider _sphere;
    private Coroutine _shootTick;
    private Wallet _wallet;

    public Tower SetupWallet(Wallet wallet)
    {
        _wallet = wallet;
        return this;
    }

    [field: SerializeField] public float Price { get; private set; }

    private void Awake()
    {
        _sphere = GetComponent<SphereCollider>();
        _ammo = Resources.Load<Ammo>("Ammo");
    }

    private void Start()
    {
        _sphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (enemy != null)
                _shootTick = StartCoroutine(ShootTick(enemy));
        }
    }

    private void OnMouseDown()
    {
        if (_wallet.TrySpend(Price))
        {
            Price *= _multipliedPrice;
            _sphere.radius *= _multipliedRadius;
        }
    }

    private IEnumerator ShootTick(Enemy enemy)
    {
        while (enemy.Health > 0)
        {
            Instantiate(_ammo, _pointAmmo.transform.position, Quaternion.identity).Shoot(enemy);
            yield return new WaitForSeconds(_shootDelay);
        }
    }
}