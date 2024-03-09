using UnityEngine;

public class Coin : MonoBehaviour, ISetupRocket
{
    [SerializeField] private int _baseValue;
    [SerializeField] private int _heightThreshold;
    [SerializeField] private int _valueIncrement;
    [SerializeField] private float _lifeTime;
    [field: SerializeField] public int Value { get; private set; }

    private void Start()
    {
        CalculateCoinValue();
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
        rocket.AddCoins(Value);
    }

    private void CalculateCoinValue()
    {
        float height = transform.position.y;
        int heightFactor = Mathf.FloorToInt(height / _heightThreshold);
        Value = _baseValue + (heightFactor * _valueIncrement);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}