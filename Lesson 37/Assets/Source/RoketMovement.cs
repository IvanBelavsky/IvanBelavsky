using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RoketMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private ShopItemSpeed shopItemSpeed;

    private Rigidbody _rigidbody;

    [field: SerializeField] public bool IsMove { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        shopItemSpeed.OnUpdateSpeed += SpeedUpdate;
        Load();
    }

    private void OnDisable()
    {
        shopItemSpeed.OnUpdateSpeed -= SpeedUpdate;
    }

    public void SetupShopItemSpeed(ShopItemSpeed itemSpeed)
    {
        shopItemSpeed = itemSpeed;
    }

    public void Move()
    {
        if (!IsMove)
            _rigidbody.AddForce(Vector3.up * _speed, ForceMode.Force);
        IsMove = true;
    }

    public void Rotate()
    {
        if (Input.GetKey(KeyCode.A))
            _rigidbody.AddForce(Vector3.left * _speedRotate, ForceMode.Impulse);
        if (Input.GetKey(KeyCode.D))
            _rigidbody.AddForce(Vector3.right * _speedRotate, ForceMode.Impulse);
    }

    private void SpeedUpdate()
    {
        _speed *= 1.2f;
        Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("Speed"))
            _speed = PlayerPrefs.GetFloat("Speed", _speed);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Speed", _speed);
    }
}