using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslate))]
public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] _healthUI;

    private PlayerHealth _player;
    private TextMeshProUGUI _text;
    private TextTranslate _textTranslate;

    [Inject]
    public void Constructor(PlayerHealth player)
    {
        _player = player;
        if (_player != null)
            _player.OnHealthChange += HealthUpdate;
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textTranslate = GetComponent<TextTranslate>();
    }

    private void Start()
    {
        _player.OnHealthChange += HealthUpdate;
    }

    private void OnDisable()
    {
        _player.OnHealthChange -= HealthUpdate;
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