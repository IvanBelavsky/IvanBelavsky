using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson_3 : MonoBehaviour
{

    [SerializeField] float _firstValue, _secondValue;


    void Start()
    {
        Result();
        Multiplication(_firstValue, _secondValue);

    }

    void Result()
    {
        Sum(2, 5);
    }
    void Sum(float a, float b)
    {
        float sum = a + b;
        Debug.Log("Сумма сложение равна = " + sum);
    }

    void Multiplication(float i, float j)
    {
        float sum = _secondValue * _firstValue;
        Debug.Log("Сумма уножения равна = " + sum);
    }

    void Calculator()
    {


        Debug.Log("Какую арифмитеское дейтсвие вы хотите совершить?\n Выберете цмфру: 1.Умножение\t 2.Деление\t 3.Сложение\t 4.Вычитание");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float sumMult = _firstValue * _secondValue;
            Debug.Log($"Вы умножили число - {_firstValue} и число - {_secondValue}. Сумма умножение калькулятора равно: {sumMult}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float sumDivision = _firstValue / _secondValue;
            Debug.Log($"Вы поделили числа - {_firstValue} и {_secondValue}. Сумма деления калькулятора равна: {sumDivision}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            float summa = _firstValue + _secondValue;
            Debug.Log($"Вы сложили число - {_firstValue} и число - {_secondValue}. Сумма сложения калькулятора равна: {summa}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            float Minus = _firstValue - _secondValue;
            Debug.Log($"Вы отняли от числа - {_firstValue} число - {_secondValue}. Сумма вычитания калькулятора равна: {Minus}");
        }
        else
        {

        }

    }
    void Update()
    {
        Calculator();
    }
}
