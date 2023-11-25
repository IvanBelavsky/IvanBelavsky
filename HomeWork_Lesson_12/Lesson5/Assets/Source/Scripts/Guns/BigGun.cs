using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGun : Gun
{
    [SerializeField] private Ball _balls;
    [SerializeField] private float _delayDoubleShoot;

    public override void Shoot()
    {
        StartCoroutine(DoubleShootTick());
    }

    private void CreateBalls()
    {
        if (Ball == 0)
        {
            CanShoot = false;
            _counter.text = "You're out of balls, press R to reload!";
            return;
        }
        CanShoot = false;
        Ball ballsCreated = Instantiate(_balls, _spawnPoint.position, Quaternion.identity).GetComponent<Ball>();
        ballsCreated.Fly(_spawnPoint.transform.forward, 50);
        AmmoCounter(Ball);
    }

    private IEnumerator DoubleShootTick()
    {
        CreateBalls();
        yield return new WaitForSeconds(_delayDoubleShoot);
        CreateBalls();
        yield return new WaitForSeconds(_delayDoubleShoot);
        CreateBalls();
        Delay();
    }
}
