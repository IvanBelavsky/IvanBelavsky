using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Bonus : MonoBehaviour, IPauseble, ISavePositionble
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _lifeTime;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private PauseService _pauseService;
    private SaveService _saveService;
    
    private bool _isPause;

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
    
    [field: SerializeField] public string ID = "BonusPosition";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = _gravity;
        _rigidbody.velocity = _velocity;
        SetID();
    }

    private void OnEnable()
    {
        _pauseService.AddPauses(this);
        _saveService.AddPosition(this);
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        _pauseService.RemovePauses(this);
        _saveService.RemovePosition(this);
    }

    public void PlayPause()
    {
        _isPause = true;
        if (_isPause)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.GetComponent<Animator>().enabled = false;
        }
    }

    public void Continue()
    {
        _isPause = false;
        if (!_isPause)
        {
            _rigidbody.gravityScale = 1;
            _rigidbody.velocity = _velocity;
            _rigidbody.GetComponent<Animator>().enabled = true;
        }
    }

    public void SavePosition()
    {
        _saveService.CurrentSaveData.AddData(ID, new BonusSaveData(transform.position, ID, typeof(Bonus)));
        _saveService.Save();
        Debug.Log("SaveBonus");
    }

    public void LoadPosition()
    {
        if (_saveService.CurrentSaveData.TryGetData<BonusSaveData>(ID, out BonusSaveData bonusSaveData))
        {
            transform.position = bonusSaveData.BonusPosition.Vector();
        }

        Debug.Log("LoadBonusPosition");
    }
    
    private void SetID()
    {
        ID = UnityEngine.Random.Range(0, 100000).ToString();
    }

}

[Serializable]
public class BonusSaveData : SaveDatas
{
    public BonusSaveData(Vector3 position, string id, Type type) : base(id, type)
    {
        BonusPosition = new VectorSerializable(position);
    }

    public VectorSerializable BonusPosition { get; private set; }
}