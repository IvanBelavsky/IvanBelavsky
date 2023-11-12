using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGun : Gun
{
    [SerializeField] private Bomm _bomms;
    [SerializeField] private float _delayDoubleShoot;

    public override void Shoot()
    {
        StartCoroutine(ShootTick());
    }

    private void DoubleShoot()
    {
        if (Ammo == 0)
        {
            CanShoot = false;
            _counter.text = "You're out of BOMMS, press R to reload!";
            return;
        }
        CanShoot = false;
        Bomm bommsCreated = Instantiate(_bomms, _spawnPoint.position, Quaternion.identity).GetComponent<Bomm>();
        bommsCreated.Fly(_spawnPoint.transform.forward, 50);
        Ammo--;
    }
    private IEnumerator ShootTick()
    {
        DoubleShoot();
        yield return new WaitForSeconds(_delayDoubleShoot);
        DoubleShoot();
        yield return new WaitForSeconds(_delayDoubleShoot);
        DoubleShoot();
        Delay();
    }
}
