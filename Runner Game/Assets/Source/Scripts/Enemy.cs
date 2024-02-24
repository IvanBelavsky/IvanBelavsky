using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Animator _animator;
    private Coroutine _dieTick;
    private ParticleSystem _particle;
    
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _particle = Resources.Load<ParticleSystem>("Effect 1");
    }

    private void Start()
    {
        float random = Random.Range(5, 10);
        Vector3 startPos = transform.position; 

        transform.DOMoveY(startPos.y + 1f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        transform.DOMoveX(startPos.x - _speed, random)
            .SetEase(Ease.Linear);
    }

    private void Update()
    {
        if (Health <= 0) 
           Die();
    }

    public void Die()
    {
        _animator.SetBool("IsDie", true);
        _dieTick = StartCoroutine(DieTick());
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;
        Health -= damage;
    }

    private IEnumerator DieTick()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}