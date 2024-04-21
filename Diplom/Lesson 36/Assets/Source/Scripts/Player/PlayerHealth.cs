using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerHealth : MonoBehaviour, IDamageble, IPauseble, ISavePositionble
{
    public event Action OnHealthChange;
    public event Action OnTakeBonus;

    [SerializeField] private bool _isDie;
    [SerializeField] private float _damage;

    private Destroyer _dieAnimation;
    private PauseService _pauseService;
    private SceneService _sceneService;
    private SaveService _saveService;

    [Inject]
    public void Constructor(PauseService pauseService, SceneService sceneService, SaveService saveService)
    {
        _pauseService = pauseService;
        _sceneService = sceneService;
        _saveService = saveService;
    }
    
    public void Setup(string id)
    {
        ID = id;
    }

    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public string ID = "PlayerPosition";

    private void Awake()
    {
        _dieAnimation = Resources.Load<Destroyer>(AssetsPath.Animation.Destroy);
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
        _saveService.AddPosition(this);
        _saveService.Setup(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(_damage);
        }

        if (other.gameObject.TryGetComponent(out Bonus bonus))
        {
            OnTakeBonus?.Invoke();
        }
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
        _saveService.RemovePosition(this);
    }
    
    public void SetHealth(float health)
    {
        Health = health;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage must be positive");
        if (_isDie)
            return;
        Health -= damage;
        OnHealthChange?.Invoke();
        if (Health <= 0)
        {
            Die();
        }
    }

    public void PlayPause()
    {
        GetComponent<PlayerMovement>().PlayPause();
        GetComponent<PlayerAttack>().PlayPause();
    }

    public void Continue()
    {
        GetComponent<PlayerMovement>().Continue();
        GetComponent<PlayerAttack>().Continue();
    }
    
    public void SavePosition()
    {
        _saveService.CurrentSaveData.AddData(ID, new PlayerSaveData(transform.position, ID, typeof(PlayerHealth)));
        _saveService.Save();
        Debug.Log("SavePosition");
    }

    public void LoadPosition()
    {
        if (_saveService.CurrentSaveData.TryGetData<PlayerSaveData>(ID, out PlayerSaveData playerSaveData))
        {
            transform.position = playerSaveData.PlayerPosition.Vector();
        }

        Debug.Log("LoadPosition");
    }

    private void DieAnimation()
    {
        Instantiate(_dieAnimation, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        _isDie = true;
        Destroy(gameObject);
        DieAnimation();
        ReloadScene();
    }

    private void ReloadScene()
    {
        _sceneService.Restart();
    }
}

[Serializable]
public class PlayerSaveData : SaveDatas
{
    public PlayerSaveData(Vector3 position, string id, Type type) : base(id, type)
    {
        PlayerPosition = new VectorSerializable(position);
    }

    public VectorSerializable PlayerPosition { get; private set; }
}