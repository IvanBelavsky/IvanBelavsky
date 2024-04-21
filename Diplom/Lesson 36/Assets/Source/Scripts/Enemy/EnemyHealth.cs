using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyShoot))]
public class EnemyHealth : MonoBehaviour, IDamageble, IPauseble, ISavePositionble
{
    public event Action OnScoreChange;
    public event Action OnCreateBonusChange;

    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private bool _isDie;
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    private PauseService _pauseService;
    private SaveService _saveService;
    private SpriteRenderer _spriteRenderer;
    private bool _isPause;
    
    private Dictionary<string, EnemyHealth> _enemyPositions = new Dictionary<string, EnemyHealth>();
    
    [Inject]
    public void Constructor(PauseService pauseService, SaveService saveService)
    {
        _pauseService = pauseService;
        _saveService = saveService;
    }

    public void Setup(string id)
    {
        ID = id;
    }

    [field: SerializeField] public string ID { get; private set; }

    private void Awake()
    {
        SetID();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyPositions[ID] = this;
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
        _saveService.AddPosition(this);
    }

    private void Start()
    {
        RandomSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(_damage);
        }
    }
    
    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
        _saveService.RemovePosition(this);
    }

    private void OnDestroy()
    {
        _saveService.CurrentSaveData.RemoveData(ID);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (_isDie)
            return;
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void PlayPause()
    {
        GetComponent<EnemyMovement>().PlayPause();
        GetComponent<EnemyShoot>().PlayPause();
    }

    public void Continue()
    {
        GetComponent<EnemyMovement>().Continue();
        GetComponent<EnemyShoot>().Continue();
    }
    
    public void SavePosition()
    {
        _saveService.CurrentSaveData.AddData(ID, new EnemySaveData(transform.position, ID, typeof(EnemyHealth)));
        _saveService.Save();
        Debug.Log("SavePosition");
    }

    public void LoadPosition()
    {
        if (_saveService.CurrentSaveData.TryGetData<EnemySaveData>(ID, out EnemySaveData enemySaveData))
        {
            if (_enemyPositions.ContainsKey(ID))
            {
                EnemyHealth enemyInstance = _enemyPositions[ID];
                enemyInstance.transform.position = enemySaveData.EnemyPosition.Vector();
                _pauseService.AddPauses(enemyInstance);
            }
        }

        Debug.Log("LoadPosition");
    }

    private void RandomSprite()
    {
        _spriteRenderer.sprite = _sprites[UnityEngine.Random.Range(0, _sprites.Count)];
    }
    
    private void SetID()
    {
        ID = UnityEngine.Random.Range(0, 100000).ToString();
    }

    private void Die()
    {
        OnScoreChange?.Invoke();
        OnCreateBonusChange?.Invoke();
        _isDie = true;
        Destroy(gameObject);
    }
}

[Serializable]
public class EnemySaveData : SaveDatas
{
    public EnemySaveData(Vector3 position, string id, Type type) : base(id, type)
    {
        EnemyPosition = new VectorSerializable(position);
    }

    public VectorSerializable EnemyPosition { get; private set; }
}