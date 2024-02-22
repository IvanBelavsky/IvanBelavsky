using TMPro;
using UnityEngine;

public class CounerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private Player _player;

    private void Awake()
    {
        _player.OnCounterChange += UpdateCounter;
    }

    private void UpdateCounter()
    {
        _counter.text = "Coins: " + _player.Coins;
    }
}
