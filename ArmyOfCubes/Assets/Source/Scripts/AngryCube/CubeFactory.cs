using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    private AngryCube _cube;
    private CubeColor _cubeColor;

    private void Awake()
    {
        _cube = Resources.Load<AngryCube>("AngryCube");
        _cubeColor = Resources.Load<CubeColor>("CubeColor");
    }

    public AngryCube CreatedCubes(Vector3 position, Transform parent)
    {
       return Instantiate(_cube, position, Quaternion.Euler(new Vector3(0, 180, 0)), parent).GetComponent<AngryCube>();
    }
    public CubeColor CreatedColorCubes(Vector3 position, Transform parent)
    {
        return Instantiate(_cubeColor, position, Quaternion.identity, parent).GetComponent<CubeColor>();
    }
}
