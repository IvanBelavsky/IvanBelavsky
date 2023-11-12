using System.Collections;
using TMPro;
using UnityEngine;
public class Gun : MonoBehaviour
{
    [SerializeField] private protected Ammo _ammo;
    [SerializeField] private protected Transform _spawnPoint;
    [SerializeField] private protected TextMeshProUGUI _counter;
    [SerializeField] private protected float _shootDelay;
    [SerializeField] private protected float _delay;
    [SerializeField] private protected float _timeReload;
    [SerializeField] private protected int _maxAmmo;
    [field: SerializeField] public int Ammo { get; private protected set; }
    [field: SerializeField] public bool CanShoot { get; private protected set; }

    private void Start()
    {
        Ammo = _maxAmmo;
    }

    public virtual void Shoot()
    {
        if (Ammo != 0 && CanShoot == true)
        {
            CanShoot = false;
            Ammo ammoCreated = Instantiate(_ammo, _spawnPoint.position, Quaternion.identity).GetComponent<Ammo>();
            ammoCreated.Fly(_spawnPoint.transform.forward, 50);
            Delay();
            Ammo--;
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
        Ammo = _maxAmmo;
    }

    private protected void Delay()
    {
        StartCoroutine(DelayTick());
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
