using System;
using UnityEngine;

public class FuelCan : MonoBehaviour, ISetupRocket
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _fuelAmount;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rocket rocket))
        {
            if (rocket != null)
                SetupRocket(rocket);
            Die();
        }
    }

    public void SetupRocket(Rocket rocket)
    {
        if (_fuelAmount <= 0)
            throw new ArgumentException("Amount must be positive");
        rocket.AddFuelAmount(_fuelAmount);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}