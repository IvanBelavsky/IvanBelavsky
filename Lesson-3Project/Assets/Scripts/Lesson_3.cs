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
        Debug.Log("����� �������� ����� = " + sum);
    }

    void Multiplication(float i, float j)
    {
        float sum = _secondValue * _firstValue;
        Debug.Log("����� �������� ����� = " + sum);
    }

    void Calculator()
    {


        Debug.Log("����� ������������ �������� �� ������ ���������?\n �������� �����: 1.���������\t 2.�������\t 3.��������\t 4.���������");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float sumMult = _firstValue * _secondValue;
            Debug.Log($"�� �������� ����� - {_firstValue} � ����� - {_secondValue}. ����� ��������� ������������ �����: {sumMult}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float sumDivision = _firstValue / _secondValue;
            Debug.Log($"�� �������� ����� - {_firstValue} � {_secondValue}. ����� ������� ������������ �����: {sumDivision}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            float summa = _firstValue + _secondValue;
            Debug.Log($"�� ������� ����� - {_firstValue} � ����� - {_secondValue}. ����� �������� ������������ �����: {summa}");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            float Minus = _firstValue - _secondValue;
            Debug.Log($"�� ������ �� ����� - {_firstValue} ����� - {_secondValue}. ����� ��������� ������������ �����: {Minus}");
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
