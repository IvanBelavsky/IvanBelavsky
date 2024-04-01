using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerHealth : MonoBehaviour, IDamageble, IPauseble
{
    public event Action OnHealthChange;
    public event Action OnTakeBonus;

    [SerializeField] private bool _isDie;
    [SerializeField] private float _damage;

    private Destroyer _dieAnimation;
   [SerializeField] private ButtonsUI _buttonsUI;

    [field: SerializeField] public float Health { get; private set; }

    private void Awake()
    {
        _dieAnimation = Resources.Load<Destroyer>("Animations/Destroy");
        GetComponent<PlayerAttack>().Setup(_buttonsUI);
    }

    private void Start()
    {
        _buttonsUI.OnClickPauseButton += PlayPause;
        _buttonsUI.OnClickPlayButton += Continue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
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
        _buttonsUI.OnClickPauseButton -= PlayPause;
        _buttonsUI.OnClickPlayButton -= Continue;
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}