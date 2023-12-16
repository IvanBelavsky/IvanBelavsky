using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _console;
    private float _health = 100f;
    private float _mana = 100f;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _health -= UnityEngine.Random.Range(10, 25);
            _healthBar.fillAmount = _health / 100;
            if (_health <= 0)
            {
                _console.color = Color.red;
                _console.text = "У вас закончилось здоровье \n Game over!";
                return;
            }
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            _mana -= UnityEngine.Random.Range(10, 25);
            _manaBar.fillAmount = _mana / 100;
            if (_mana <= 0)
            {
                _console.color = Color.grey;
                _console.text = "У вас закончилась мана \n Пополните ману!";
                return;
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            _manaBar.fillAmount = 100;
            _mana = 100f;
            _console.color = Color.blue;
            _console.text = "Вы пополнили ману на 100%";
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            _healthBar.fillAmount = 100;
            _health = 100f;
            _console.color = Color.green;
            _console.text = "Вы пополнили здоровье на 100%";
        }
    }
}
