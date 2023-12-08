using DG.Tweening;
using UnityEngine;

public class Disolve : MonoBehaviour
{
    [SerializeField] private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _material.DOFloat(0, "_Value", 4);
        }
    }
}
