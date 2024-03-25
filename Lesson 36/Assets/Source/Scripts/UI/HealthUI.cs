using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    private TextMeshProUGUI _text;
    private float _health;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _player.OnHealthChange += HealthUpdate;
        _health = _player.Health;
        _text.text = "Health: " + _health;
    }

    private void OnDisable()
    {
        _player.OnHealthChange -= HealthUpdate;
    }

    public void Setup(PlayerHealth player)
    {
        _player = player;
        if (_player != null)
            _player.OnHealthChange += HealthUpdate;
    }

    private void HealthUpdate()
    {
        _text.text = "Health: " + _player.Health;
    }
}