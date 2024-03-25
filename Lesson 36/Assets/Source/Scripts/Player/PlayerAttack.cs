using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _delay;

    private AmmoPlayer _ammoPlayer;
    private AudioSource _audioSource;
    private Coroutine _spawnTick;
    private bool _isCanAttack;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _ammoPlayer = Resources.Load<AmmoPlayer>("Ammo/Ammo");
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isCanAttack)
        {
            _spawnTick = StartCoroutine(SpawnTick());
            AudioAttack();
            _isCanAttack = true;
        }
    }

    private void AudioAttack()
    {
        _audioSource.clip = Resources.Load<AudioClip>("Audio/Attack");
        _audioSource.pitch = Random.Range(2.5f, 3.5f);
        _audioSource.Play();
    }

    private IEnumerator SpawnTick()
    {
        Instantiate(_ammoPlayer, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_delay);
        _isCanAttack = false;
    }
}