using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeFactory))]
public class Spawner : MonoBehaviour
{
    private List<AngryCube> _cubes = new List<AngryCube>();
    private List<CubeColor> _cubeColor = new List<CubeColor>();
    private CubeFactory _factory;
    private Coroutine _moveTick;
    private Coroutine _moveTickColorCubes;

    private void Awake()
    {
        _factory = GetComponent<CubeFactory>();
    }

    private void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30));
            Vector3 randomPositionColorCube = transform.position + new Vector3(Random.Range(-50, 60), 0, Random.Range(-50, 60));
            _cubes.Add(_factory.CreatedCubes(randomPosition, transform));
            _cubeColor.Add(_factory.CreatedColorCubes(randomPosition + new Vector3(10, 0, 10), transform));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moveTick = StartCoroutine(MoveTick());
            _moveTickColorCubes = StartCoroutine(MoveTickColorCubes());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (_cubes != null && _cubeColor != null)
            {
                StopCoroutine(_moveTick);
                _moveTick = null;
                StopCoroutine(_moveTickColorCubes);
                _moveTickColorCubes = null;
            }
        }
    }

    private IEnumerator MoveTick()
    {
        while (true)
        {
            foreach (AngryCube cubes in _cubes)
            {
                yield return new WaitForSeconds(0.25f);
                cubes.Move(-Vector3.forward);
                yield return new WaitForSeconds(1);
                cubes.Jump(Vector3.up, -Vector3.forward);
            }
        }
    }
    private IEnumerator MoveTickColorCubes()
    {
        while (true)
        {
            foreach (CubeColor color in _cubeColor)
            {
                yield return new WaitForSeconds(0.5f);
                color.MoveColorCube(-Vector3.forward).Color();
            }
        }
    }
}
