using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HealthAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("HealthAnimation");
    }

    private void Update()
    {
        _animator.Play("HealthAnimation");
    }
}
