using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Image[] _healthUI;
    
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _player.OnHealthChange += HealthUpdate;
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
        for (int i = 0; i < _healthUI.Length; i++)
        {
            if (i < _player.Health)
            {
                _healthUI[i].enabled = true;
            }
            else
            {
                _healthUI[i].enabled = false;
            }
        }
    }
}