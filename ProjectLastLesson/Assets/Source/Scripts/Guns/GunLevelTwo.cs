using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLevelTwo : Gun
{
    [SerializeField] private Ball _balls;
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
            _counter.text = "You're out of balls, press R to reload!";
            return;
        }
        CanShoot = false;
        Ball ballsCreated = Instantiate(_balls, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
        ballsCreated.Fly(_spawnPoint.transform.forward, 50);
        Ammo--;
    }

    private IEnumerator ShootTick()
    {
        DoubleShoot();
        yield return new WaitForSeconds(_delayDoubleShoot);
        DoubleShoot();
        Delay();
    }
}
