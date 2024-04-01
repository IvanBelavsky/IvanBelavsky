using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _delay;

    private AmmoPlayer _ammoPlayer;
    private AudioSource _audioSource;
    private Coroutine _spawnTick;
    [SerializeField]private ButtonsUI _buttonsUI;
    private bool _isCanAttack;
    private bool _isPause;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _ammoPlayer = Resources.Load<AmmoPlayer>("Ammo/Ammo");
    }

    private void Start()
    {
        _ammoPlayer.Setup(_buttonsUI);
    }

    private void Update()
    {
        Attack();
    }

    private void OnDisable()
    {
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
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