using TMPro;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _dropdown.onValueChanged.AddListener(StatePlayer);
    }

    private void StatePlayer(int index)
    {
        if (_dropdown.value == 0)
        {
            _animator.Play("Idle");
        }
        if (_dropdown.value == 1)
        {
            _animator.Play("Run");
        }
        if (_dropdown.value == 2)
        {
            _animator.Play("Jump");
        }
    }
}

