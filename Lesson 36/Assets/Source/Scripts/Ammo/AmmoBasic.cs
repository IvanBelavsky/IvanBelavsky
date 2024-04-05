using UnityEngine;
using UnityEngine.Audio;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class AmmoBasic : MonoBehaviour, IPauseble
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _startSpeed;
    [SerializeField] protected float _damage;
    [SerializeField] protected bool _isPause;
    [SerializeField] private float _lifeTime;

    private protected PauseService _pauseService;
    protected Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private Destroyer _hitAnimation;
    
    [Inject]
    public void Consctuctor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _hitAnimation = Resources.Load<Destroyer>(AssetsPath.Animation.Hit);
        _startSpeed = _speed;
        _pauseService.AddPauses(this);
    }

    private void Update()
    {
        Move();
        DieTime();
    }
    
    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
    }

    public virtual void Move()
    {
        _rigidbody.velocity = Vector2.up * _speed;
    }

    public void PlayPause()
    {
        _isPause = true;
        if (_isPause)
        {
            _rigidbody.gravityScale = 0;
            _speed = 0;
            _rigidbody.GetComponent<Animator>().enabled = false;
        }
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
        {
            _rigidbody.gravityScale = 1;
            _speed = _startSpeed;
            _rigidbody.GetComponent<Animator>().enabled = true;
        }
    }

    protected void Die()
    {
        AudioHit();
        HitAnimation();
        Destroy(gameObject);
    }

    private void HitAnimation()
    {
        Instantiate(_hitAnimation, transform.position, Quaternion.identity);
    }

    private void AudioHit()
    {
        GameObject newAudio = new GameObject();
        newAudio.transform.position = transform.position;
        AudioSource audioSource = newAudio.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>(AssetsPath.Audio.AudioMixer);
        audioSource.clip = Resources.Load<AudioClip>(AssetsPath.Audio.Hit);
        audioSource.Play();
        Destroy(newAudio, 1);
    }

    private void DieTime()
    {
        if (!_isPause)
            Destroy(gameObject, _lifeTime);
    }
}