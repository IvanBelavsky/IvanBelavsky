using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Gun _gunLevelOne;
    [SerializeField] private GunLevelTwo _gunLevelTwo;
    [SerializeField] private LastGun _lastGun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _gun.CanShoot)
        {
            _gun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gun.Relaod();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gunLevelOne.gameObject.SetActive(true);
            _gunLevelTwo.gameObject.SetActive(false);
            _lastGun.gameObject.SetActive(false);
            _gun = _gunLevelOne;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _gunLevelOne.gameObject.SetActive(false);
            _gunLevelTwo.gameObject.SetActive(true);
            _lastGun.gameObject.SetActive(false);
            _gun = _gunLevelTwo;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _gunLevelOne.gameObject.SetActive(false);
            _gunLevelTwo.gameObject.SetActive(false);
            _lastGun.gameObject.SetActive(true);
            _gun = _lastGun;
        }
    }
}
