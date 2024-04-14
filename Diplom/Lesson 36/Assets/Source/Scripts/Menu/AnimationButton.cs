using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class AnimationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetBool("IsClick", false);
        _animator.Play(AssetsPath.Animation.ClickButton);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _animator.SetBool("IsClick", true);
        _animator.Play(AssetsPath.Animation.ApButton);
    }
}
