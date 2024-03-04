using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class AmmoBasic : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _damage;
    [SerializeField] private float _lifeTime;

    protected Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private Destroyer _hitAnimation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _hitAnimation = Resources.Load<Destroyer>("Hit");
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        _rigidbody.velocity = Vector2.up * _speed;
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
        audioSource.clip = Resources.Load<AudioClip>("Hit");
        audioSource.Play();
        Destroy(newAudio, 1);
    }

    protected void Die()
    {
        AudioHit();
        HitAnimation();
        Destroy(gameObject);
    }
}