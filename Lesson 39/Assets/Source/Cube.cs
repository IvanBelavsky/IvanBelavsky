using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private List<TextMeshPro> _valueNumbers = new List<TextMeshPro>();
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;

    private MeshRenderer _renderer;
    
    [field: SerializeField] public int Value { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        Value = Random.Range(_minValue, _maxValue);
    }

    private void Start()
    {
        SetValue();
        SetColor();
    }

    public void Multiplay(int value)
    {
        Value += value;
        SetValue();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.Value == Value)
            {
                Multiplay(cube.Value);
                SetColor();
                Destroy(cube.gameObject);
            }
        }
    }

    private void SetValue()
    {
        foreach (TextMeshPro value in _valueNumbers)
        {
            value.text = Value.ToString();
        }
    }
    
    private void SetColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}