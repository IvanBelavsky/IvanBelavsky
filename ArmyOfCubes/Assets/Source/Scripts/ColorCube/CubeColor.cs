using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeColor : MonoBehaviour
{
    [SerializeField] private float _speedSlow;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public CubeColor MoveColorCube(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speedSlow;
        return this;
    }

    public CubeColor Color()
    {
        _meshRenderer.material.color = Random.ColorHSV();
        return this;
    }
}
