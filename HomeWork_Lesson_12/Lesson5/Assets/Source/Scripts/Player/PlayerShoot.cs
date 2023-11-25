using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Gun _smallGun;
    [SerializeField] private Gun _MediumGun;
    [SerializeField] private Gun _BigGun;

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
            _smallGun.gameObject.SetActive(true);
            _MediumGun.gameObject.SetActive(false);
            _BigGun.gameObject.SetActive(false);
            _gun = _smallGun;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _smallGun.gameObject.SetActive(false);
            _MediumGun.gameObject.SetActive(true);
            _BigGun.gameObject.SetActive(false);
            _gun = _MediumGun;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _smallGun.gameObject.SetActive(false);
            _MediumGun.gameObject.SetActive(false);
            _BigGun.gameObject.SetActive(true);
            _gun = _BigGun;
        }
    }
}
