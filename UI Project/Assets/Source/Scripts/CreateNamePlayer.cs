using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNamePlayer : MonoBehaviour
{
    [SerializeField] private Image _person;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _button;
    [SerializeField] private string[] _names;
    [SerializeField] private TextMeshProUGUI _console;

    private void Awake()
    {
        Sprite sprite = Resources.Load<Sprite>("Player");
        _person.sprite = sprite;
        _button.onClick.AddListener(CreateName);
    }

    private void CreateName()
    {
        for (int i = 0; i < _names.Length; i++)
        {
            if (_inputField.text.ToLower() == _names[i].ToLower())
            {
                _console.text = "Имя " + _inputField.text + " уже занято";
                _console.color = Color.red;
                _console.fontSize = 26;
                return;
            }
        }
        if (_inputField.text.Length >= 10 || _inputField.text.Length <= 2)
        {
            _console.fontSize = 12;
            _console.color = Color.black;
            _console.text = "Неверный формат имени, имя должно содержать больше 2 смволов и быть не более 10 символов";
            return;
        }
        _console.fontSize = 26;
        _console.color = Color.green;
        _console.text = "Вы задали имя - " + _inputField.text;
        _name.text = _inputField.text;
        _inputField.text = "";
    }
}
