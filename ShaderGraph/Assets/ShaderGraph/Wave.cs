using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private Material _wave;

    private void Awake()
    {
        _wave = GetComponent<MeshRenderer>().materials[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _wave.SetFloat("_Speed", 15);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _wave.SetFloat("_Speed", 5);
        }

    }
}
