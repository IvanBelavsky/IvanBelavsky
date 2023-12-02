using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeColor : Cube
{
    [SerializeField] private float _speedSlow;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void Move(Vector3 direction)
    {
        _rigidbody.velocity = direction * _speedSlow;
    }

    public CubeColor SetRandomColor()
    {
        _meshRenderer.material.color = Random.ColorHSV();
        return this;
    }
}
