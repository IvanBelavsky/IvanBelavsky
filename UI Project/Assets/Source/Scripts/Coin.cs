using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputFieldPlus;
    [SerializeField] private TMP_InputField _inputFieldMinus;
    [SerializeField] private Button _buttonPlus;
    [SerializeField] private Button _buttonMinus;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private Image _coin;
    private int _count = 0;

    private void Awake()
    {
        Sprite sprite = Resources.Load<Sprite>("Coin");
        _coin.sprite = sprite;
        _buttonPlus.onClick.AddListener(Sum);
        _buttonMinus.onClick.AddListener(Subtraction);
    }

    private void Update()
    {

    }

    private void Subtraction()
    {
        _count -= Convert.ToInt32(_inputFieldMinus.text);
        _inputText.text = _count.ToString();
        _inputFieldMinus.text = "";
    }

    private void Sum()
    {
        _count += Convert.ToInt32(_inputFieldPlus.text);
        _inputText.text = _count.ToString();
        _inputFieldPlus.text = "";
    }
}
