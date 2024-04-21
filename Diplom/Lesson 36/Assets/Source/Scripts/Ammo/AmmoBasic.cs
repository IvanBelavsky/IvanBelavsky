using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class AmmoBasic : MonoBehaviour, IPauseble, ISavePositionble
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _startSpeed;
    [SerializeField] protected float _damage;
    [SerializeField] protected bool _isPause;
    [SerializeField] private float _lifeTime;

    private protected Dictionary<string, AmmoBasic> _ammoPositions = new Dictionary<string, AmmoBasic>();
    private protected PauseService _pauseService;
    private protected SaveService _saveService;
    protected Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private Destroyer _hitAnimation;
    private AmmoType _ammoType;

    [Inject]
    public void Consctuctor(PauseService pauseService, SaveService saveService)
    {
        _pauseService = pauseService;
        _saveService = saveService;
    }
    
    public void Setup(string id)
    {
        ID = id;
    }
    
    [field: SerializeField] protected string ID { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _hitAnimation = Resources.Load<Destroyer>(AssetsPath.Animation.Hit);
        _startSpeed = _speed;
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
        _saveService.AddPosition(this);
        ID = UnityEngine.Random.Range(0, 100000).ToString();
        _ammoPositions[ID] = this;
    }

    private void Update()
    {
        Move();
        DieTime();
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
        _saveService.RemovePosition(this);
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
    
    public void SavePosition()
    {
        _saveService.CurrentSaveData.AddData(ID, new AmmoSaveData(transform.position, ID, _ammoType));
        _saveService.Save();
        Debug.Log("SavePosition");
    }

    public void LoadPosition()
    {
        if (_saveService.CurrentSaveData.TryGetData<AmmoSaveData>(ID, out AmmoSaveData ammoSaveData))
        {
            if (_ammoPositions.ContainsKey(ID))
            {
                AmmoBasic ammoInstance = _ammoPositions[ID];
                ammoInstance.transform.position = ammoSaveData.Position.Vector();
                ammoInstance._ammoType = ammoSaveData.AmmoType;
            }
        }

        Debug.Log("LoadPosition");
    }

    protected void SetType(AmmoType ammoType)
    {
        _ammoType = ammoType;
    }
    
    protected void SetID()
    {
        ID = UnityEngine.Random.Range(0, 100000).ToString();
    }

    protected void Die()
    {
        AudioHit();
        HitAnimation();
        Destroy(gameObject);
    }

    private void HitAnimation()
    {
        if (!_isPause) 
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

[Serializable]
public class AmmoSaveData : SaveDatas
{
    public AmmoSaveData(Vector3 position, string id, AmmoType ammoType) : base(id, typeof(AmmoBasic))
    {
        Position = new VectorSerializable(position);
        AmmoType = ammoType;
    }

    public VectorSerializable Position { get; private set; }
    public AmmoType AmmoType { get; private set; }
}

public enum AmmoType
{
    playerType = 0,
    enemyType = 1
}