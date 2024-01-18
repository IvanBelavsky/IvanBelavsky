using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IEntryPointSetupPlayer
{
    [SerializeField] private Transform _player;

    private NavMeshAgent _agent;
    private EnemyHealth _enemyHealth;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _animator.Play("Run");
    }

    private void Update()
    {
        if (!_player)
            return;
        _agent.SetDestination(_player.position);
    }

    public void Setup(PlayerMovement player)
    {
        _player = player.transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth health))
        {
            if (!health.IsTookBonus)
                health.TakeDamage(1);
            else
                _enemyHealth.TakeDamage(1);
        }
    }

    public Enemy Speed(float speed)
    {
        transform.GetComponent<NavMeshAgent>().speed = speed;
        return this;
    }
}