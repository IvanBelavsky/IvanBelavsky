using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _seconds = 3;
    [SerializeField] private TextMeshProUGUI _text;

    private Player _player;
    private Enemy _enemy;
    private Coroutine _timeTick;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _enemy = GetComponent<Enemy>();
        _player.OnClickButton += StartTimer;
    }

    private void StartTimer() => _timeTick = StartCoroutine(TimerTick());

    private IEnumerator TimerTick()
    {
        while (_seconds > 0)
        {
            yield return new WaitForSeconds(1);
            _seconds--;
            _text.text = $"Time: {_seconds.ToString()}";
        }

        _enemy.RandomType();
        _player.Check();
    }
}