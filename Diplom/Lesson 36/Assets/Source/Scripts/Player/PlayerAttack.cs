using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(FactoryAmmo))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _delay;

    private AmmoPlayer _ammoPlayer;
    private AudioSource _audioSource;
    private FactoryAmmo _factoryAmmo;
    private Coroutine _spawnTick;
    private DiContainer _diContainer;
    private bool _isCanAttack;
    private bool _isPause;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        _factoryAmmo = GetComponent<FactoryAmmo>();
        _audioSource = GetComponent<AudioSource>();
        _ammoPlayer = Resources.Load<AmmoPlayer>(AssetsPath.Ammo.PlayerAmmo);
    }

    private void Update()
    {
        Attack();
    }

    public void PlayPause()
    {
        _isPause = true;
    }

    public void Continue()
    {
        _isPause = false;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isCanAttack && !_isPause)
        {
            _spawnTick = StartCoroutine(SpawnTick());
            AudioAttack();
            _isCanAttack = true;
        }
    }

    private void AudioAttack()
    {
        _audioSource.clip = Resources.Load<AudioClip>(AssetsPath.Audio.Attack);
        _audioSource.pitch = Random.Range(2.5f, 3.5f);
        _audioSource.Play();
    }

    private IEnumerator SpawnTick()
    {
        AmmoPlayer ammoPlayer = _factoryAmmo.CreatedPlayerAmmo(transform.position);
        //  _diContainer.InstantiatePrefab(_ammoPlayer, transform.position, Quaternion.identity, null)
        //  .GetComponent<AmmoPlayer>();
        yield return new WaitForSeconds(_delay);
        _isCanAttack = false;
    }
}